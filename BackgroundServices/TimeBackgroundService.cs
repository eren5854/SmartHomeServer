using Microsoft.EntityFrameworkCore;
using SmartHomeServer.Context;
using SmartHomeServer.Enums;
using SmartHomeServer.Models;

namespace SmartHomeServer.BackgroundServices;

public class TimeBackgroundService(
    ApplicationDbContext context)
{
    public void TimeTrigger()
    {
        var scenarios = context.Scenarios.Include(i => i.Trigger).ThenInclude(i => i!.Action).ToList();

        foreach (var scenario in scenarios)
        {
            TriggerTypeEnum triggerType = scenario.Trigger!.TriggerType;
            DateTime? triggerTime = scenario.Trigger.TriggerTime;
            if (triggerType == TriggerTypeEnum.Time && triggerTime.HasValue)
            {
                TimeSpan triggerTimeSpan = triggerTime.Value.TimeOfDay;
                TimeSpan currentTimeSpan = DateTime.Now.TimeOfDay;

                TimeSpan lowerBound = currentTimeSpan.Subtract(TimeSpan.FromMinutes(2));
                TimeSpan upperBound = currentTimeSpan.Add(TimeSpan.FromMinutes(2));

                Console.WriteLine(currentTimeSpan);

                if (triggerTimeSpan >= lowerBound && triggerTimeSpan <= upperBound)
                {
                    ActionTypeEnum actionType = scenario.Trigger.Action!.ActionType;
                    double? actionValue = scenario.Trigger.Action.Value;
                    Guid sensorId = scenario.Trigger.Action.SensorId;

                    Sensor? sensor = context.Sensors.Where(p => p.Id == sensorId).FirstOrDefault();
                    if (sensor is null)
                    {
                        throw new ArgumentException("Sensor bulunamadı");
                    }

                    if(sensor.Data1 != actionValue)
                    {
                        sensor.Data1 = actionValue;

                        context.Update(sensor);
                        context.SaveChanges();
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

            if(triggerType == TriggerTypeEnum.Value)
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
                    Guid actionSensorId = scenario.Trigger.Action.SensorId;
                    double actionValue = scenario.Trigger.Action.Value;
                    Sensor? actionSensor = context.Sensors.Where(p => p.Id == actionSensorId).FirstOrDefault();
                    if (actionSensor is null)
                    {
                        throw new ArgumentException("Sensor bulunamadı");
                    }

                    if(actionSensor.Data1 != actionValue)
                    {
                        actionSensor.Data1 = actionValue;

                        context.Update(actionSensor);
                        context.SaveChanges();

                        Console.WriteLine($"{scenario.ScenarioName} senaryo içerisindeki {scenario.Trigger.TriggerType} tetiklendi ve {scenario.Trigger.Action.SensorId} sensörün Data1 verisi güncellendi");
                    }
                    else
                    {
                        Console.WriteLine("Sensör zaten istenen konumda");
                    }

                }
                else
                {
                    Console.WriteLine("İstenen şartlar sağlanamdı");
                }
            }
            else
            {
                Console.WriteLine("Değişiklik yok");
            }

        }
    }
}
