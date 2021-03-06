﻿namespace EmployeeManagement.Domain.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public SettingsModel SettingsModel { get; set; }
        public string SecurityStamp { get; set; }
    }
}
