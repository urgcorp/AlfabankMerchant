﻿namespace AlfabankMerchant.ComponentModel
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ActionPropertyAttribute : Attribute
    {
        /// <summary>
        /// Property name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Property is required
        /// <para>Sometimes require parameter to be sent, but value can be empty</para>
        /// </summary>
        public bool Required { get; }

        /// <summary>
        /// Property allow empty value if is required
        /// </summary>
        public bool AllowEmpty { get; set; }

        /// <summary>
        /// Data type complaint with ISO 8583
        /// </summary>
        public string? Type { get; set; }

        public ActionPropertyAttribute(string propertyName, bool required = false)
        {
            Name = propertyName;
            Required = required;
        }
    }
}
