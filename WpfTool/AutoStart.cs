﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfTool
{
    internal class AutoStart
    {

        public static bool GetStatus()
        {
            try
            {
                RegistryKey R_local = Registry.CurrentUser;
                RegistryKey R_run = R_local.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                object value = R_run.GetValue("WpfTool");
                R_run.Close();
                R_local.Close();
                if (value == null)
                {
                    return false;
                }
                return value.ToString().Equals(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public static void Enable()
        {
            try
            {
                RegistryKey R_local = Registry.CurrentUser;
                RegistryKey R_run = R_local.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                R_run.SetValue("WpfTool", System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
                R_run.Close();
                R_local.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("设置失败，可能需要您使用管理员权限启动应用后再修改");
            }
        }

        public static void Disable()
        {
            try
            {
                RegistryKey R_local = Registry.CurrentUser;
                RegistryKey R_run = R_local.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                R_run.DeleteValue("WpfTool", false);
                R_run.Close();
                R_local.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("设置失败，可能需要您使用管理员权限启动应用后再修改");
            }
        }

    }
}
