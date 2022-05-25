// See https://aka.ms/new-console-template for more information

using System;
using System.Diagnostics;
using System.Linq;

var timeGap = TimeSpan.FromSeconds(10);

var processList = Process.GetProcesses().Where(x => x.ProcessName.Contains("Notepad")).ToList();

bool ShouldKill(Process process)
    => (DateTime.Now - process.StartTime) > timeGap;

foreach (var process in processList.Where(ShouldKill)) process.Kill();