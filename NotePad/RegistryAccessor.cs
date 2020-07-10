using System;
using Microsoft.Win32;

namespace NotePad
{
    public class RegistryAccessor
    {
        /// <summary>
        /// A property to show or hide error messages 
        /// (default = false)
        /// </summary>
        public bool ShowError { get; set; } = false;

        /// <summary>
        /// A property to set the SubKey value
        /// (default = "SOFTWARE\\" + Application.ProductName.ToUpper())
        /// </summary>
        public string SubKey { get; set; } = "SOFTWARE\\TomsNotePad";

        /// <summary>
        /// A property to set the BaseRegistryKey value.
        /// (default = Registry.LocalMachine)
        /// </summary>
        public RegistryKey BaseRegistryKey { get; set; } = Registry.LocalMachine;

        /// <summary>
        /// To read a registry key.
        /// input: KeyName (string)
        /// output: value (string) 
        /// </summary>
        public string Read(string KeyName)
        {
            // Opening the registry key
            var baseRegisteryKey = BaseRegistryKey;
            // Open a subKey as read-only
            var subRegisteryKey = baseRegisteryKey.OpenSubKey(SubKey);
            // If the RegistrySubKey doesn't exist -> (null)
            if (subRegisteryKey == null)
            {
                return null;
            }
            else
            {
                try
                {
                    // If the RegistryKey exists I get its value
                    // or null is returned.
                    return (string)subRegisteryKey.GetValue(KeyName.ToUpper());
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex, "Reading registry " + KeyName.ToUpper());
                    return null;
                }
            }
        }

        /// <summary>
        /// To write into a registry key.
        /// input: KeyName (string) , Value (object)
        /// output: true or false 
        /// </summary>
        public bool Write(string keyName, object value)
        {
            try
            {
                // Setting
                var baseRegistryKey = BaseRegistryKey;
                var subRegistryKey = baseRegistryKey.CreateSubKey(SubKey);
                // Save the value
                subRegistryKey.SetValue(keyName.ToUpper(), value);

                return true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex, "Writing registry " + keyName.ToUpper());
                return false;
            }
        }

        /// <summary>
        /// To delete a registry key.
        /// input: KeyName (string)
        /// output: true or false 
        /// </summary>
        public bool DeleteKey(string KeyName)
        {
            try
            {
                // Setting
                var baseRegistryKey = BaseRegistryKey;
                var subRegistryKey = baseRegistryKey.CreateSubKey(SubKey);
                // If the RegistrySubKey doesn't exists -> (true)
                if (subRegistryKey == null)
                    return true;
                else
                    subRegistryKey.DeleteValue(KeyName);

                return true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex, "Deleting SubKey " + SubKey);
                return false;
            }
        }

        /// <summary>
        /// To delete a sub key and any child.
        /// input: void
        /// output: true or false 
        /// </summary>
        public bool DeleteSubKeyTree()
        {
            try
            {
                // Setting
                var baseRegistryKey = BaseRegistryKey;
                var subRegistryKey = baseRegistryKey.OpenSubKey(SubKey);
                // If the RegistryKey exists, delete it
                if (subRegistryKey != null)
                    baseRegistryKey.DeleteSubKeyTree(SubKey);

                return true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex, "Deleting SubKey " + SubKey);
                return false;
            }
        }

        /// <summary>
        /// Retrive the count of subkeys at the current key.
        /// input: void
        /// output: number of subkeys
        /// </summary>
        public int SubKeyCount()
        {
            try
            {
                // Setting
                var baseRegistryKey = BaseRegistryKey;
                var subRegistryKey = baseRegistryKey.OpenSubKey(SubKey);
                // If the RegistryKey exists...
                if (subRegistryKey != null)
                    return subRegistryKey.SubKeyCount;
                else
                    return 0;
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex, "Retriving subkeys of " + SubKey);
                return 0;
            }
        }

        /// <summary>
        /// Retrive the count of values in the key.
        /// input: void
        /// output: number of keys
        /// </summary>
        public int ValueCount()
        {
            try
            {
                // Setting
                var baseRegistryKey = BaseRegistryKey;
                var subRegistryKey = baseRegistryKey.OpenSubKey(SubKey);
                // If the RegistryKey exists...
                if (subRegistryKey != null)
                    return subRegistryKey.ValueCount;
                else
                    return 0;
            }
            catch (Exception ex)
            {
                
                ShowErrorMessage(ex, "Retriving keys of " + SubKey);
                return 0;
            }
        }

        private void ShowErrorMessage(Exception ex, string title)
        {
            //	if (showError == true)

        }

    }
}
