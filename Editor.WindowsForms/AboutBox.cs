// <copyright file="AboutBox.cs" company="Shkyrockett" >
// Copyright © 2016 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Reflection;

namespace Editor.WindowsForms;

/// <summary>
/// 
/// </summary>
/// <seealso cref="Form" />
partial class AboutBox
    : Form
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AboutBox"/> class.
    /// </summary>
    public AboutBox()
    {
        InitializeComponent();
        Text = $"About {AssemblyTitle}";
        labelProductName.Text = AssemblyProduct;
        labelVersion.Text = $"Version {AssemblyVersion}";
        labelCopyright.Text = AssemblyCopyright;
        labelCompanyName.Text = AssemblyCompany;
        textBoxDescription.Text = AssemblyDescription;
    }

    #region Assembly Attribute Accessors
    /// <summary>
    /// Gets the assembly title.
    /// </summary>
    /// <value>
    /// The assembly title.
    /// </value>
    public static string AssemblyTitle
    {
        get
        {
            var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            if (attributes.Length > 0)
            {
                var titleAttribute = (AssemblyTitleAttribute)attributes[0];
                if (titleAttribute.Title != string.Empty)
                {
                    return titleAttribute.Title;
                }
            }
            return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().Location);
        }
    }

    /// <summary>
    /// Gets the assembly version.
    /// </summary>
    /// <value>
    /// The assembly version.
    /// </value>
    public static string AssemblyVersion
    {
        get
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
    }

    /// <summary>
    /// Gets the assembly description.
    /// </summary>
    /// <value>
    /// The assembly description.
    /// </value>
    public static string AssemblyDescription
    {
        get
        {
            var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            return attributes.Length == 0 ? string.Empty : ((AssemblyDescriptionAttribute)attributes[0]).Description;
        }
    }

    /// <summary>
    /// Gets the assembly product.
    /// </summary>
    /// <value>
    /// The assembly product.
    /// </value>
    public static string AssemblyProduct
    {
        get
        {
            var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
            return attributes.Length == 0 ? string.Empty : ((AssemblyProductAttribute)attributes[0]).Product;
        }
    }

    /// <summary>
    /// Gets the assembly copyright.
    /// </summary>
    /// <value>
    /// The assembly copyright.
    /// </value>
    public static string AssemblyCopyright
    {
        get
        {
            var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
            return attributes.Length == 0 ? string.Empty : ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
        }
    }

    /// <summary>
    /// Gets the assembly company.
    /// </summary>
    /// <value>
    /// The assembly company.
    /// </value>
    public static string AssemblyCompany
    {
        get
        {
            var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
            return attributes.Length == 0 ? string.Empty : ((AssemblyCompanyAttribute)attributes[0]).Company;
        }
    }
    #endregion
}
