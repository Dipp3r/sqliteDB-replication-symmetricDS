using System;
using System.Collections.Generic;

namespace SQLiteReplicaServer2.Server2.Models;

public partial class Alarm
{
    public int AlarmId { get; set; }

    public string? Name { get; set; }
}
