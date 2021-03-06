﻿using Boilerplate.App.Services.General.Contracts;
using Boilerplate.Common.Models;
using Newtonsoft.Json;
using System;
using Xamarin.Essentials;

namespace Boilerplate.App.Services.General
{
    public class AppSettings : IAppSettings
    {
        private const string ServerUrlKey = "ApiServerUrl";
        private const string CurrentUserKey = "CurrentUser";
        private const string CompanyNameKey = "CompanyName";
        private const string AccessTokenKey = "AccessToken";

        public UserModel CurrentUser
        {
            get => GetItem<UserModel>(CurrentUserKey);
            set => AddItem(CurrentUserKey, value);
        }        
        public string CompanyName
        {
            get => GetItem(CompanyNameKey);
            set => AddItem(CompanyNameKey, value);
        }
        public string ServerUrlSetting
        {
            get => GetItem(ServerUrlKey);
            set => AddItem(ServerUrlKey, value);
        }
        public string AccessToken
        {
            get => GetItem(AccessTokenKey);
            set => AddItem(AccessTokenKey, value);
        }

        #region Private Methods
        private void AddItem(string key, string value)
        {
            try
            {
                Preferences.Set(key, value);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding '{key}' preference: {ex.Message}");
            }
        }

        private string GetItem(string key)
        {
            var preferenceValue = string.Empty;
            try
            {
                preferenceValue = Preferences.Get(key, string.Empty);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving value for key '{key}': {ex.Message}");
            }

            return preferenceValue;
        }

        private void AddItem<T>(string key, T value)
        {
            try
            {
                var serialized = JsonConvert.SerializeObject(value);
                Preferences.Set(key, serialized);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error serializing {key} with value {value}: {ex.Message}");
            }
        }

        private T GetItem<T>(string key)
        {
            T item = default;
            try
            {
                var val = Preferences.Get(key, string.Empty);
                item = JsonConvert.DeserializeObject<T>(val);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deserializing {key} to type {typeof(T)}: {ex.Message}");
            }

            return item;
        }
        #endregion
    }
}