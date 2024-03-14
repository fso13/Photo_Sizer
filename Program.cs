// Decompiled with JetBrains decompiler
// Type: Photo_Sizer.Program
// Assembly: Photo_Sizer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9B3EADE7-A949-4521-8B64-665FCD02EB88
// Assembly location: C:\Users\dimar\OneDrive\Рабочий стол\Photo_Sizer.exe

using System;
using System.Windows.Forms;

namespace Photo_Sizer
{
  internal static class Program
  {
    [STAThread]
    private static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run((Form) new Form1());
    }
  }
}
