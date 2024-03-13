using SQLiteReplica.Server1.Models;

while (true) {
    try
    {
        Console.Write("\n1 - Read all alarms\n2 - Add alarm\n3 - Update an alarm\n4 - Delete an alarm\nEnter the operation you want to perform:");
        int choice = Convert.ToInt32(Console.ReadLine());
        switch (choice) {
            case 1:
                List < Alarm > alarmList = ReadAlarm();
                Console.WriteLine("----List of alarms----");
                foreach (var alarm in alarmList)
                {
                    Console.WriteLine($"Alarm Id: {alarm.AlarmId}, Name: {alarm.Name}");
                }
                break;
            case 2:
                Console.Write("Enter the name of the alarm: ");
                string alarmName = Console.ReadLine();
                Random random = new();
                int randomId = random.Next(10000000, 99999999);
                Alarm newAlarm = new()
                {
                    AlarmId = randomId,
                    Name = alarmName
                };
                bool res = AddAlarm(newAlarm);
                if (res)
                {
                    Console.WriteLine("The alarm was added sucessfully");
                }
                else {
                    Console.WriteLine("Error adding the alarm");   
                }
                break;
            case 3:
                Console.Write("Enter the Id of the alarm you want to update: ");
                int id = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter the new name: ");
                string name = Console.ReadLine();
                Alarm updatedAlarm = new()
                {
                    AlarmId = id,
                    Name = name
                };
                bool updateRes = UpdateAlarm(updatedAlarm);
                if (updateRes)
                {
                    Console.WriteLine("The alarm was updated sucessfully");
                }
                else
                {
                    Console.WriteLine("Error updating the alarm");
                };
                break;
            case 4:
                Console.Write("Enter the Id of the alarm you want to delete: ");
                int deletetionId = Convert.ToInt32(Console.ReadLine());
                bool deleteRes = DeleteAlarm(deletetionId);
                if (deleteRes)
                {
                    Console.WriteLine("The alarm was deleted sucessfully");
                }
                else
                {
                    Console.WriteLine("Error deleting the alarm");
                }
                break;
            default:
                Console.WriteLine("Enter a valid value between (1-4)");
                break;

        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        if (e.InnerException != null) Console.WriteLine(e.InnerException.Message);
    }
}


//Read the alarms
static List<Alarm> ReadAlarm() {
    using var db = new PrimaryContext();
    var alarmList = db.Alarms.ToList();
    return alarmList;
}

//Add Alarm
static bool AddAlarm(Alarm item)
{
    try
    {
        using var db = new PrimaryContext();
        db.Alarms.Add(item);
        db.SaveChanges();
        return true;
    }
    catch(Exception err)
    {
        Console.WriteLine(err);
        return false;
    }
}


//Delete the alarm
static bool DeleteAlarm(int AlarmId)
{
    try
    {
        using var db = new PrimaryContext();
        var item = db.Alarms.Find(AlarmId);
        if (item == null) return false;
        else {
            db.Alarms.Remove(item);
            db.SaveChanges();
            return true;
        }
    }
    catch (Exception err) { 
        Console.WriteLine(err);
        return false;
    }
}


//Update the price of the item
static bool UpdateAlarm(Alarm item)
{
    try
    {
        using var db = new PrimaryContext();
        db.Alarms.Update(item);
        db.SaveChanges();
        return true;
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        return false;
    }
}
