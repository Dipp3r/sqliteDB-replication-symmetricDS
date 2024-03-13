using System;
using System.Collections.Generic;

namespace SQLiteReplica.Server1.Models;

public partial class Alarm
{
    public int AlarmId { get; set; }

    public string? Name { get; set; }
}
