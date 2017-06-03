﻿namespace Library
{
    using System;
    using System.Collections.Generic;
    using Helper;
    using Resource;

    public class Newspaper : ItemCatalog
    {
        private const int YearPublication = 1900;
        private const int DefaultYear = 2000;
        private const byte DefaultNumber = 1;
        private string publisher;
        private int year;
        private int number;
        private DateTime date;

        public Newspaper(List<string> aboutItemCatalog)
        {
            this.Id = ItemCatalog.GetId();
            this.errorList = new List<string>();
            this.Create(aboutItemCatalog);
        }

        public string PublisherCity { get; set; }

        public string Publisher
        {
            get
            {
                return this.publisher;
            }

            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.publisher = value;
                }
                else
                {
                    this.publisher = Titles.DefaultPublisher;
                    this.errorList.Add(string.Format(Titles.PublisherError, this.publisher));
                }
            }
        }

        public int Year
        {
            get
            {
                return this.year;
            }

            set
            {
                if (value >= Newspaper.YearPublication)
                {
                    this.year = value;
                }
                else
                {
                    this.year = Newspaper.DefaultYear;
                    this.errorList.Add(string.Format(Titles.YearError, this.year));
                }
            }
        }

        public int Number
        {
            get
            {
                return this.number;
            }

            set
            {
                if (value > 0)
                {
                    this.number = value;
                }
                else
                {
                    this.number = Newspaper.DefaultNumber;
                    this.errorList.Add(string.Format(Titles.NumberNewsError, this.number));
                }
            }
        }

        public DateTime Date
        {
            get
            {
                return this.date;
            }

            set
            {
                if (value != DateTime.MinValue)
                {
                    this.date = value;
                }
                else
                {
                    this.date = DateTime.Today;
                    this.errorList.Add(string.Format(Titles.DateNewsError, this.date.ToShortDateString()));
                }
            }
        }

        public string ISSN { get; set; }

        public override string TypeItem
        {
            get
            {
                return Titles.TypeNews;
            }
        }

        public override List<string> GetQuestionAboutItem
        {
            get
            {
                return Helper.GetyQuestions(Titles.AskAboutNewspaper);
            }
        }

        internal override int PublishedYear
        {
            get
            {
                return this.Year;
            }
        }

        protected override Dictionary<string, string> TitleasAndValuesItem
        {
            get
            {
                Dictionary<string, string> titleasAndValuesItem = new Dictionary<string, string>();

                titleasAndValuesItem.Add(Titles.Title, this.Title);
                titleasAndValuesItem.Add(Titles.City, this.PublisherCity);
                titleasAndValuesItem.Add(Titles.Publisher, this.Publisher);
                titleasAndValuesItem.Add(Titles.Year, this.Year.ToString());
                titleasAndValuesItem.Add(Titles.PageCount, this.PageCount.ToString());
                titleasAndValuesItem.Add(Titles.Note, this.Note);
                titleasAndValuesItem.Add(Titles.Number, this.Number.ToString());
                titleasAndValuesItem.Add(Titles.Date, this.Date.ToShortDateString());
                titleasAndValuesItem.Add(Titles.ISSN, this.ISSN);

                return titleasAndValuesItem;
            }
        }

        public override string ToString()
        {
            return base.ToString()
                       .Insert(
                       0,
                       string.Format(Titles.AboutItem, this.Id.ToString(), Titles.TypeNews));
        }

        internal override string GetInfoToSave()
        {
            return base.GetInfoToSave().Insert(0, Titles.TypeNews);
        }

        protected internal override void Create(List<string> aboutItemCatalog)
        {
            var intValue = 0;
            DateTime date;

            this.Title = aboutItemCatalog[0];
            this.PublisherCity = aboutItemCatalog[1];
            this.Publisher = aboutItemCatalog[2];

            Helper.IsIntMoreThanZero(aboutItemCatalog[3], out intValue);
            this.Year = intValue;

            Helper.IsIntMoreThanZero(aboutItemCatalog[4], out intValue);
            this.PageCount = intValue;

            this.Note = aboutItemCatalog[5];

            Helper.IsIntMoreThanZero(aboutItemCatalog[6], out intValue);
            this.Number = intValue;

            Helper.IsDateAsDDMMYYYY(aboutItemCatalog[7], out date);
            this.Date = date;

            this.ISSN = aboutItemCatalog[8];
        }
    }
}