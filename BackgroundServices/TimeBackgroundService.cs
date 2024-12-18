using ED.GenericEmailService.Models;
using ED.GenericEmailService;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SmartHomeServer.Context;
using SmartHomeServer.Enums;
using SmartHomeServer.Hubs;
using SmartHomeServer.Models;
using ED.Result;

namespace SmartHomeServer.BackgroundServices;

public class TimeBackgroundService(
    ApplicationDbContext context,
    IHubContext<SensorHub> hubContext)
{
    public void TimeTrigger()
    {
        var scenarios = context.Scenarios.Include(i => i.Trigger).ThenInclude(i => i!.Action).ToList();

        foreach (var scenario in scenarios)
        {
            TriggerTypeEnum triggerType = scenario.Trigger!.TriggerType;
            DateTime? triggerTime = scenario.Trigger.TriggerTime;
            if (triggerType == TriggerTypeEnum.Time && triggerTime.HasValue && scenario.IsActive == true)
            {
                TimeSpan triggerTimeSpan = triggerTime.Value.TimeOfDay;
                TimeSpan currentTimeSpan = DateTime.Now.TimeOfDay;

                TimeSpan lowerBound = currentTimeSpan.Subtract(TimeSpan.FromSeconds(30));
                TimeSpan upperBound = currentTimeSpan.Add(TimeSpan.FromSeconds(30));

                Console.WriteLine(currentTimeSpan);

                if (triggerTimeSpan >= lowerBound && triggerTimeSpan <= upperBound)
                {
                    ActionTypeEnum actionType = scenario.Trigger.Action!.ActionType;
                    double? actionValue = scenario.Trigger.Action.Value;
                    Guid? sensorId = scenario.Trigger.Action.SensorId;

                    Sensor? sensor = context.Sensors.Where(p => p.Id == sensorId).FirstOrDefault();
                    if (sensor is null)
                    {
                        throw new ArgumentException("Sensor bulunamadı");
                    }

                    if (sensor.Data1 != actionValue)
                    {
                        sensor.Data1 = actionValue;

                        context.Update(sensor);
                        Notification notification = new()
                        {
                            NotificationName = $"{sensor.SensorName} çalıştı",
                            NotificationMessage = $"{triggerTime} zamanlı tetiklenme mekanizması sonucu {sensor.SensorName} adlı cihaz çalıştırıldı",
                            NotificationType = NotificationTypeEnum.info,
                            AppUserId = scenario.AppUserId,
                            IsActive = true,
                            IsDeleted = false,
                            CreatedDate = DateTime.Now,
                            CreatedBy = "Background Service"
                        };
                        context.Add(notification);
                        context.SaveChanges();
                        hubContext.Clients.All.SendAsync("Lights", Result<Sensor>.Succeed(sensor));
                        hubContext.Clients.All.SendAsync("Notifications", Result<Notification>.Succeed(notification));
                        Console.WriteLine($"{scenario.ScenarioName} içerisindeki {scenario.Trigger.TriggerType} tetiklendi ve {scenario.Trigger.Action.SensorId} sensörün Data1 verisi güncellendi");
                    }
                    else
                    {
                        Console.WriteLine("Sensör zaten istenen konumda");
                    }
                }
                else
                {
                    Console.WriteLine("İstenen şartlar sağlanamadı");
                }
            }
            else
            {
                Console.WriteLine("Değişiklik yok");

            }
        }
    }

    public void ValueTrigger()
    {
        var scenarios = context.Scenarios.Include(i => i.Trigger).ThenInclude(i => i!.Action).ToList();
        foreach (var scenario in scenarios)
        {
            TriggerTypeEnum triggerType = scenario.Trigger!.TriggerType;
            double? triggerValue = scenario.Trigger.TriggerValue;

            if (triggerType == TriggerTypeEnum.Value && scenario.IsActive == true)
            {
                Guid? triggerSensorId = scenario.Trigger.SensorId;

                Sensor? triggerSensor = context.Sensors.Where(p => p.Id == triggerSensorId).FirstOrDefault();
                if (triggerSensor is null)
                {
                    throw new ArgumentException("Sensor bulunamadı");
                }

                if (triggerSensor.Data1 == triggerValue)
                {
                    ActionTypeEnum actionType = scenario.Trigger.Action!.ActionType;
                    if (actionType == ActionTypeEnum.Active)
                    {

                        Guid? actionSensorId = scenario.Trigger.Action.SensorId;
                        double actionValue = scenario.Trigger.Action.Value;
                        Sensor? actionSensor = context.Sensors.Where(p => p.Id == actionSensorId).FirstOrDefault();
                        if (actionSensor is null)
                        {
                            throw new ArgumentException("Sensor bulunamadı");
                        }

                        if (actionSensor.Data1 != actionValue)
                        {
                            actionSensor.Data1 = actionValue;

                            context.Update(actionSensor);

                            Notification notification = new()
                            {
                                NotificationName = $"{actionSensor.SensorName} çalıştı",
                                NotificationMessage = $"{triggerSensor.SensorName} adlı sensörün tetiklenme mekanizması sonucu {actionSensor.SensorName} adlı cihaz çalıştırıldı",
                                NotificationType = NotificationTypeEnum.warning,
                                AppUserId = scenario.AppUserId,
                                IsActive = true,
                                IsDeleted = false,
                                CreatedDate = DateTime.Now,
                                CreatedBy = "Background Service"
                            };
                            context.Add(notification);

                            context.SaveChanges();
                            hubContext.Clients.All.SendAsync("Lights", Result<Sensor>.Succeed(actionSensor));
                            hubContext.Clients.All.SendAsync("Notifications", Result<Notification>.Succeed(notification));

                            Console.WriteLine($"{scenario.ScenarioName} senaryo içerisindeki {scenario.Trigger.TriggerType} tetiklendi ve {scenario.Trigger.Action.SensorId} sensörün Data1 verisi güncellendi");
                        }
                        else
                        {
                            Console.WriteLine("Sensör zaten istenen konumda");
                        }
                    }
                    else if (actionType == ActionTypeEnum.Email)
                    {
                        MailSetting? mailSetting = context.MailSettings.Where(p => p.AppUserId == scenario.AppUserId).FirstOrDefault();
                        if(mailSetting is not null)
                        {
                            Notification notification = new()
                            {
                                NotificationName = $"E-posta gönderildi",
                                NotificationMessage = $"{triggerSensor.SensorName} adlı sensörün tetiklenme mekanizması sonucu {mailSetting.Email} adresine mail gönderildi",
                                NotificationType = NotificationTypeEnum.errorr,
                                AppUserId = scenario.AppUserId,
                                IsActive = true,
                                IsDeleted = false,
                                CreatedDate = DateTime.Now,
                                CreatedBy = "Background Service"
                            };
                            context.Add(notification);
                            context.SaveChanges();
                            hubContext.Clients.All.SendAsync("Notifications", Result<Notification>.Succeed(notification));

                            string body = CreateBody(triggerSensor.SensorName, triggerSensor.Data1);
                            string subject = "Uyarı!!";

                            EmailConfigurations emailConfigurations = new(
                              mailSetting!.SmtpDomainName,
                              //"smtp.gmail.com",
                              mailSetting.AppPassword,
                              //"bwtfwpainnkmyxmn",
                              mailSetting.SmtpPort,
                              //465,
                              true,
                              true);

                            EmailModel<Stream> emailModel = new(
                                emailConfigurations,
                                mailSetting.Email,
                                new List<string> { mailSetting.Email ?? "" },
                                subject,
                                body,
                                null);

                            EmailService.SendEmailWithMailKitAsync(emailModel);

                           
                            Console.WriteLine("Mail başarılı bir şekilde gönderildi");
                        }
                        Console.WriteLine("Mail ayarları bulunamadı");
                    }
                }
                else
                {
                    Console.WriteLine("Tetikleyici tetiklenmedi");
                }
            }
            else
            {
                Console.WriteLine("Değişiklik yok");
            }

        }
    }

    private string CreateBody(string sensorName, double? value)
    {
        string body = $@"Senaryoda oluşturduğunuz {sensorName} adlı sensor {value} durumunda tetiklendi";
        return body;
    }
}
