﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LibraryApp {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class AllTextRus {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal AllTextRus() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("LibraryApp.AllTextRus", typeof(AllTextRus).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Укажите название книги:,Укажите авторов книги:,Укажите город издания книги:,Укажите название издательства книги:,Укажите год издания книги:,Укажите количество страниц книги:,Укажите примечание к книге:,Укажите ISBN книги:.
        /// </summary>
        internal static string AskAboutBook {
            get {
                return ResourceManager.GetString("AskAboutBook", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Укажите название газеты:,Укажите город издания газеты:,Укажите название издательства газеты:,Укажите год издания газеты:,Укажите количество страниц газеты:,Укажите примечание к газете:,Укажите номер газеты:,Укажите дату газеты:,Укажите ISSN газеты:.
        /// </summary>
        internal static string AskAboutNewspaper {
            get {
                return ResourceManager.GetString("AskAboutNewspaper", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Укажите название патента:,Укажите изобретателей патента:,Укажите страну патента:,Укажите регистрационный номер патента:,Укажите дату подачи заяки патента:,Укажите дату публикации патента:,Укажите количество страниц патента:,Укажите примечание к патенту:.
        /// </summary>
        internal static string AskAboutPatent {
            get {
                return ResourceManager.GetString("AskAboutPatent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Укажите номер для удаления из Каталога:.
        /// </summary>
        internal static string AskToDelete {
            get {
                return ResourceManager.GetString("AskToDelete", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Вы удалили запись #{0}.
        ///Нажмите любую кнопку для выходя в Главное меню....
        /// </summary>
        internal static string Delete {
            get {
                return ResourceManager.GetString("Delete", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Главное меню:
        ///[1]-- Добавить запись в Каталог
        ///[2]-- Удалить запись из Каталога
        ///
        ///[Q] - Закончить работу с Каталогом.
        /// </summary>
        internal static string MainMenu {
            get {
                return ResourceManager.GetString("MainMenu", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Добавить в Каталог:
        ///[1]-- Книга
        ///[2]-- Газета
        ///[3]-- Патент
        ///
        ///[Q] - Перейти в Главное меню.
        /// </summary>
        internal static string MenuToAdd {
            get {
                return ResourceManager.GetString("MenuToAdd", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Не существует записи #{0}.
        ///Нажмите любую кнопку для выходя в Главное меню....
        /// </summary>
        internal static string NoDelete {
            get {
                return ResourceManager.GetString("NoDelete", resourceCulture);
            }
        }
    }
}
