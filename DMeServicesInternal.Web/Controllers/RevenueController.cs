using DMeServices.Models;
using DMeServices.Models.Common.Revenue;
using DMeServices.Models.Revenue;
using DMeServices.Models.ViewModels.Revenue;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace DMeServicesInternal.Web.Controllers
{
    public class RevenueController : BaseController
    {
        // GET: Revenue
        public ActionResult Index()
        {
                return View();
        }

        public ActionResult RevenueDetails(int ID)
        {
            RevenueViewModel oModel = new RevenueViewModel();
            oModel.RevMainPayment = RevenueCom.RevenueDataById(ID);
            if(oModel.RevMainPayment.PaymentType=="D")
            {
                oModel.RevMainPayment.PaymentType = "22";
            }
            else
            {
                oModel.RevMainPayment.PaymentType = "23";
            }
            ViewBag.DDDepartmentByEmployeeSiteCd = DDDepartmentByEmployeeSiteCd();
            ViewBag.DDWelayatByDepartmentID = DDWelayatByDepartmentID();
            ViewBag.DDFinancialItems = DDFinancial_Items();
            ViewBag.DDSubFinancialItems = DDSubFinancialItems(int.Parse(oModel.RevMainPayment.FinancialItemId.ToString()));
            ViewBag.DDPaymentTypes = DDPaymentTypes();
            ViewBag.DDBanks = DDBanks();
            return View(oModel);
        }

        public ActionResult Revenue_Checkout()
        {
            ViewBag.DDDepartmentByEmployeeSiteCd = DDDepartmentByEmployeeSiteCd();
            ViewBag.DDWelayatByDepartmentID = DDWelayatByDepartmentID();
            ViewBag.DDFinancialItems = DDFinancialItems();
            ViewBag.DDSubFinancialItems = DDSubFinancialItems(0);
            ViewBag.DDPaymentTypes = DDPaymentTypes();
            ViewBag.DDBanks = DDBanks();
            return View();
        }

        public ActionResult Rev_Contract()
        {
            //CalculateDateDifference dateDiff = new CalculateDateDifference(DateTime.Parse("02/28/2019"), DateTime.Parse("03/01/2018"));
            ViewBag.DDDepartmentByEmployeeSiteCd = DDDepartmentByEmployeeSiteCd();
            ViewBag.DDWelayatByDepartmentID = DDWelayatByDepartmentID();
            ViewBag.DDFinancialItems = DDContractFinancialItem();
            ViewBag.DDSubFinancialItems = DDSubFinancialItems(11041101);
            ViewBag.DDPaymentTypes = DDPaymentTypes();
            ViewBag.DDContractTypes = DDContractTypes();
            ViewBag.DDBanks = DDBanks();
            return View();
        }

        #region Method :: DD Dhofar Department

        public static List<SelectListItem> DDDepartmentByEmployeeSiteCd()
        {
            RevenueViewModel oModel = new RevenueViewModel();
            List<SelectListItem> LstDepartmentByEmployeeSiteCd = new List<SelectListItem>();
            List<RevDepartment> AllDepartmentByEmployeeSiteCd = RevenueCom.DepartmentByEmployeeSiteCd(oModel.oEmployeeInfo.SiteCd);
            if (AllDepartmentByEmployeeSiteCd.Count > 0)
            {
                foreach (var item in AllDepartmentByEmployeeSiteCd)
                {
                    LstDepartmentByEmployeeSiteCd.Add(new SelectListItem() { Text = item.Code + " - " + item.Name, Value = item.Id.ToString() });
                }

            }
            return LstDepartmentByEmployeeSiteCd;
        }

        #endregion

        #region Method :: DD Welayat By DepartmentID

        public static List<SelectListItem> DDWelayatByDepartmentID()
        {
            int SiteCd = 2;
            RevenueViewModel oModel = new RevenueViewModel();
            List<RevDepartment> AllDepartmentByEmployeeSiteCd = RevenueCom.DepartmentByEmployeeSiteCd(oModel.oEmployeeInfo.SiteCd);
            foreach (var item in AllDepartmentByEmployeeSiteCd)
            {
                SiteCd = item.Id;
            }
            List<SelectListItem> LstWelayatByDepartmentID = new List<SelectListItem>();
            List<RevWelyats> WelayatByDepartmentID = RevenueCom.WelayatByDepartmentID(SiteCd);
            if (WelayatByDepartmentID.Count > 0)
            {
                foreach (var item in WelayatByDepartmentID)
                {
                    LstWelayatByDepartmentID.Add(new SelectListItem() { Text = item.Code + " - " + item.Name, Value = item.Id.ToString() });
                }

            }
            return LstWelayatByDepartmentID;
        }

        #endregion

        #region Method :: DD Financial Items

        public static List<SelectListItem> DDFinancialItems()
        {
            List<SelectListItem> LstFinancialItems = new List<SelectListItem>();
            List<RevFinancialItem> AllFinancialItems = RevenueCom.AllFinancialItems();
            if (AllFinancialItems.Count > 0)
            {
                foreach (var item in AllFinancialItems)
                {
                    LstFinancialItems.Add(new SelectListItem() { Text = item.Code + " - " + item.Name, Value = item.Code.ToString() });
                }

            }
            return LstFinancialItems;
        }

        public static List<SelectListItem> DDFinancial_Items()
        {
            List<SelectListItem> LstFinancialItems = new List<SelectListItem>();
            List<RevFinancialItem> AllFinancialItems = RevenueCom.All_FinancialItems();
            if (AllFinancialItems.Count > 0)
            {
                foreach (var item in AllFinancialItems)
                {
                    LstFinancialItems.Add(new SelectListItem() { Text = item.Code + " - " + item.Name, Value = item.Code.ToString() });
                }

            }
            return LstFinancialItems;
        }

        #endregion

        #region Method :: DD Contract Financial Item

        public static List<SelectListItem> DDContractFinancialItem()
        {
            List<SelectListItem> LstContractFinancialItem = new List<SelectListItem>();
            List<RevFinancialItem> ContractFinancialItem = RevenueCom.ContractsFinancialItem();
            if (ContractFinancialItem.Count > 0)
            {
                foreach (var item in ContractFinancialItem)
                {
                    LstContractFinancialItem.Add(new SelectListItem() { Text = item.Code + " - " + item.Name, Value = item.Code.ToString() });
                }

            }
            return LstContractFinancialItem;
        }

        #endregion

        #region Method :: DD Sub Financial Items BY FinancialID

        public static List<SelectListItem> DDSubFinancialItems(int FinancialID)
        {
            List<SelectListItem> LstSubFinancialItems = new List<SelectListItem>();
            List<RevFinancialSubItem> AllSubFinancialItems = RevenueCom.FinancialSubItemsByID(FinancialID);
            if (AllSubFinancialItems.Count > 0)
            {
                foreach (var item in AllSubFinancialItems)
                {
                    LstSubFinancialItems.Add(new SelectListItem() { Text = item.Id + " - " + item.Name, Value = item.Id.ToString() });
                }

            }
            return LstSubFinancialItems;
        }

        #endregion

        #region Method :: DD Sub Financial Items BY RevenueID

        public static List<SelectListItem> DDSubFinancialItemsBYRevenueID(int RevenueID)
        {
            List<SelectListItem> LstSubFinancialItems = new List<SelectListItem>();
            List<RevFinancialSubItem> AllSubFinancialItems = RevenueCom.FinancialSubItemsByID(RevenueID);
            if (AllSubFinancialItems.Count > 0)
            {
                foreach (var item in AllSubFinancialItems)
                {
                    LstSubFinancialItems.Add(new SelectListItem() { Text = item.Name, Value = item.Code.ToString() });
                }

            }
            return LstSubFinancialItems;
        }

        #endregion

        #region Method :: DD Sub Financial Items

        [HttpPost]
        public JsonResult DDSubFinancial_Items(int FinancialID)
        {
            List<SelectListItem> LstSubFinancialItems = new List<SelectListItem>();
            List<RevFinancialSubItem> AllSubFinancialItems = RevenueCom.FinancialSubItemsByID(FinancialID);

            return Json(AllSubFinancialItems, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Method :: DD Payment Types

        public static List<SelectListItem> DDPaymentTypes()
        {
            List<SelectListItem> LstPaymentTypes = new List<SelectListItem>();
            List<LookupType> AllPaymentTypes = RevenueCom.PaymentTypes();
            if (AllPaymentTypes.Count > 0)
            {
                foreach (var item in AllPaymentTypes)
                {
                    LstPaymentTypes.Add(new SelectListItem() { Text = item.LookupNameAr, Value = item.LookupId.ToString() });
                }

            }
            return LstPaymentTypes;
        }

        #endregion

        #region Method :: DD Contract Types

        public static List<SelectListItem> DDContractTypes()
        {
            List<SelectListItem> LstContractTypes = new List<SelectListItem>();
            List<LookupType> AllContractTypes = RevenueCom.ContractTypes();
            if (AllContractTypes.Count > 0)
            {
                foreach (var item in AllContractTypes)
                {
                    LstContractTypes.Add(new SelectListItem() { Text = item.LookupNameAr, Value = item.LookupId.ToString() });
                }

            }
            return LstContractTypes;
        }

        #endregion

        #region Method :: DD Banks

        public static List<SelectListItem> DDBanks()
        {
            List<SelectListItem> LstBanks = new List<SelectListItem>();
            List<RevBank> AllBanks = RevenueCom.Banks();
            if (AllBanks.Count > 0)
            {
                foreach (var item in AllBanks)
                {
                    LstBanks.Add(new SelectListItem() { Text = item.Name, Value = item.Code.ToString() });
                }

            }
            return LstBanks;
        }

        #endregion

        #region Method :: Save Main Payments

        public ActionResult SaveMainPayments(RevenueViewModel oModel)
        {
            long vResult = RevenueCom.SaveMainPayments(oModel);
            ViewBag.TranseID = vResult;
            return View("SaveRevenueSuccessPage");
        }

        #endregion

        #region Method :: Update Main Payments
        public ActionResult UpdateMainPayments(RevenueViewModel oModel)
        {
            long vResult = RevenueCom.UpdateMainPayments(oModel);

            ViewBag.TranseID = vResult;
            return View("SaveRevenueSuccessPage");
        }

        #endregion

        #region Method :: Revenue Search

        public ActionResult RevenueSearch(RevenueViewModel oModel)
        {
            List<RevMainPayment> RevenueSearch_Data = RevenueCom.RevenueDataByCode(Convert.ToInt32(oModel.RevMainPayment.PaymentCode), Convert.ToInt32(oModel.RevMainPayment.FinancialYear));
            if (RevenueSearch_Data.Count > 0)
            {
                int Id = 0;
                foreach (var item in RevenueSearch_Data)
                {
                    Id = int.Parse(item.Id.ToString());
                }
                return RedirectToRoute(new
                {
                    controller = "Revenue",
                    action = "RevenueDetails",
                    id= Id
                });
            }
            else
            {
                return RedirectToRoute(new
                {
                    controller = "Revenue",
                    action = "Index",
                    id = 0
                });
            }
        }

        #endregion

        [HttpPost]
        public JsonResult GetArabicPayment(string TotalPayment)
        {
            String Result = ConvertNumberToAlpha(TotalPayment);

            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DateDifference(DateTime dt1, DateTime dt2)
        {
            CalculateDateDifference dateDiff = new CalculateDateDifference(dt1, dt2);

            return Json(dateDiff, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ContractFees(double MonthValue, int Years, int Months, int Days, int Type)
        {
            double TotalValue = CalculateContractFees(MonthValue, Years, Months, Days, Type);

            return Json(TotalValue, JsonRequestBehavior.AllowGet);
        }

        #region Get Arabic Payment

        private string CorrectNumber(string Number)
        {
            string Correct = "";
            if (Number.Contains('.'))
            {
                int Index = Number.IndexOf('.');
                int Count = Number.Substring(Index + 1).Count();
                if (Count == 1)
                {
                    Correct = Number + "00";
                }
                else if (Count == 2)
                {
                    Correct = Number + "0";
                }
                else if (Count == 3)
                {
                    Correct = Number;
                }
            }
            else
            {
                Correct = Number + ".000";
            }
            return Correct;
        }

        private string ConvertNumberToAlpha(string Number)
        {
            int Index = Number.IndexOf('.');
            string Count = Number.Substring(Index + 1);

            if (Number.Contains('.') & Count !="0" & Count != "00" & Count != "000")
            {
                Number = CorrectNumber(Number);
                if (Number.Split('.')[0].ToCharArray().Length > 10)
                {
                    return "رقم غير صحيح";
                }
                else
                {
                    switch (Number.Split('.')[0].ToCharArray().Length)
                    {
                        case 0: return convertThreeDigits(Number.Split('.')[1]) + " بيسة " + "فقط";
                        case 1: return convertOneDigits(Number.ToString()) + " ريال " + " و " + convertThreeDigits(Number.Split('.')[1]) + " بيسة "+ "فقط";
                        case 2: return convertTwoDigits(Number.ToString()) + " ريال " + " و " + convertThreeDigits(Number.Split('.')[1]) + " بيسة " + "فقط";
                        case 3: return convertThreeDigits(Number.ToString()) + " ريال " + " و " + convertThreeDigits(Number.Split('.')[1]) + " بيسة " + "فقط";
                        case 4: return convertFourDigits(Number.ToString()) + " ريال " + " و " + convertThreeDigits(Number.Split('.')[1]) + " بيسة " + "فقط";
                        case 5: return convertFiveDigits(Number.ToString()) + " ريال " + " و " + convertThreeDigits(Number.Split('.')[1]) + " بيسة " + "فقط";
                        case 6: return convertSixDigits(Number.ToString()) + " ريال " + " و " + convertThreeDigits(Number.Split('.')[1]) + " بيسة " + "فقط";
                        default: return "";
                    }
                }
            }
            else
            {
                if (Number.ToCharArray().Length > 10)
                {
                    return "رقم غير صحيح";
                }
                else
                {
                    if (Count == "0" || Count == "00" || Count == "000")
                        Number = Number.Substring(0, Index);
                    switch (Number.ToCharArray().Length)
                    {
                        case 1: return convertOneDigits(Number.ToString()) + " ريال فقط ";
                        case 2: return convertTwoDigits(Number.ToString()) + " ريال فقط ";
                        case 3: return convertThreeDigits(Number.ToString()) + " ريال فقط ";
                        case 4: return convertFourDigits(Number.ToString()) + " ريال فقط ";
                        case 5: return convertFiveDigits(Number.ToString()) + " ريال فقط ";
                        case 6: return convertSixDigits(Number.ToString()) + " ريال فقط ";
                        default: return "";
                    }

                }
            }
        }
        private string convertTwoDigits(string TwoDigits)
        {
            string returnAlpha = "00";
            if (TwoDigits.ToCharArray()[0] == '0' && TwoDigits.ToCharArray()[1] != '0')
            {
                return convertOneDigits(TwoDigits.ToCharArray()[1].ToString());
            }
            else
            {
                switch (int.Parse(TwoDigits.ToCharArray()[0].ToString()))
                {
                    case 1:
                        {
                            if (int.Parse(TwoDigits.ToCharArray()[1].ToString()) == 1)
                            {
                                return "إحدى عشر";
                            }
                            else if (int.Parse(TwoDigits.ToCharArray()[1].ToString()) == 2)
                            {
                                return "إثنى عشر";
                            }
                            else
                            {
                                returnAlpha = "عشر";
                                return convertOneDigits(TwoDigits.ToCharArray()[1].ToString()) + " " + returnAlpha;
                            }
                        }
                    case 2: returnAlpha = "عشرون"; break;
                    case 3: returnAlpha = "ثلاثون"; break;
                    case 4: returnAlpha = "أربعون"; break;
                    case 5: returnAlpha = "خمسون"; break;
                    case 6: returnAlpha = "ستون"; break;
                    case 7: returnAlpha = "سبعون"; break;
                    case 8: returnAlpha = "ثمانون"; break;
                    case 9: returnAlpha = "تسعون"; break;
                    default: returnAlpha = ""; break;
                }
            }
            if (convertOneDigits(TwoDigits.ToCharArray()[1].ToString()).Length == 0)
            { return returnAlpha; }
            else
            {
                return convertOneDigits(TwoDigits.ToCharArray()[1].ToString()) + " و " + returnAlpha;
            }
        }
        private string convertOneDigits(string OneDigits)
        {
            switch (int.Parse(OneDigits.Substring(0, 1)))
            {
                case 1: return "واحد";
                case 2: return "إثنان";
                case 3: return "ثلاثه";
                case 4: return "أربعه";
                case 5: return "خمسه";
                case 6: return "سته";
                case 7: return "سبعه";
                case 8: return "ثمانيه";
                case 9: return "تسعه";
                default: return "";
            }
        }
        private string convertThreeDigits(string ThreeDigits)
        {
            switch (int.Parse(ThreeDigits.ToCharArray()[0].ToString()))
            {
                case 1:
                    {
                        if (int.Parse(ThreeDigits.ToCharArray()[1].ToString()) == 0)
                        {
                            if (int.Parse(ThreeDigits.ToCharArray()[2].ToString()) == 0)
                            {
                                return "مائه";
                            }
                            return "مائه" + " و " + convertOneDigits(ThreeDigits.ToCharArray()[2].ToString());
                        }
                        else
                        {
                            return "مائه" + " و " + convertTwoDigits(ThreeDigits.Substring(1, 2));
                        }
                    }
                case 2:
                    {
                        if (int.Parse(ThreeDigits.ToCharArray()[1].ToString()) == 0)
                        {
                            if (int.Parse(ThreeDigits.ToCharArray()[2].ToString()) == 0)
                            {
                                return "مائتين";
                            }
                            return "مائتين" + " و " + convertOneDigits(ThreeDigits.ToCharArray()[2].ToString());
                        }
                        else
                        {
                            return "مائتين" + " و " + convertTwoDigits(ThreeDigits.Substring(1, 2));
                        }
                    }
                case 3:
                    {
                        if (int.Parse(ThreeDigits.ToCharArray()[1].ToString()) == 0)
                        {
                            if (int.Parse(ThreeDigits.ToCharArray()[2].ToString()) == 0)
                            {
                                return convertOneDigits(ThreeDigits.ToCharArray()[0].ToString()).Split('ه')[0] + "مائه";
                            }
                            return convertOneDigits(ThreeDigits.ToCharArray()[0].ToString()).Split('ه')[0] + "مائه" + " و " + convertOneDigits(ThreeDigits.ToCharArray()[2].ToString());
                        }
                        else
                        {
                            return convertOneDigits(ThreeDigits.ToCharArray()[0].ToString()).Split('ه')[0] + "مائه" + " و " + convertTwoDigits(ThreeDigits.Substring(1, 2));
                        }
                    }
                case 4:
                    {
                        goto case 3;
                    }
                case 5:
                    {
                        goto case 3;
                    }
                case 6:
                    {
                        goto case 3;
                    }
                case 7:
                    {
                        goto case 3;
                    }
                case 8:
                    {
                        goto case 3;
                    }
                case 9:
                    {
                        goto case 3;
                    }
                case 0:
                    {
                        if (ThreeDigits.ToCharArray()[1] == '0')
                        {
                            if (ThreeDigits.ToCharArray()[2] == '0')
                            {
                                return "";
                            }
                            else
                            {
                                return convertOneDigits(ThreeDigits.ToCharArray()[2].ToString());
                            }
                        }
                        else
                        {
                            return convertTwoDigits(ThreeDigits.Substring(1, 2));
                        }
                    }
                default: return "";
            }
        }
        private string convertFourDigits(string FourDigits)
        {
            switch (int.Parse(FourDigits.ToCharArray()[0].ToString()))
            {
                case 1:
                    {
                        if (int.Parse(FourDigits.ToCharArray()[1].ToString()) == 0)
                        {
                            if (int.Parse(FourDigits.ToCharArray()[2].ToString()) == 0)
                            {
                                if (int.Parse(FourDigits.ToCharArray()[3].ToString()) == 0)
                                    return "ألف";
                                else
                                {
                                    return "ألف" + " و " + convertOneDigits(FourDigits.ToCharArray()[3].ToString());
                                }
                            }
                            return "ألف" + " و " + convertTwoDigits(FourDigits.Substring(2, 2));
                        }
                        else
                        {
                            return "ألف" + " و " + convertThreeDigits(FourDigits.Substring(1, 3));
                        }
                    }
                case 2:
                    {
                        if (int.Parse(FourDigits.ToCharArray()[1].ToString()) == 0)
                        {
                            if (int.Parse(FourDigits.ToCharArray()[2].ToString()) == 0)
                            {
                                if (int.Parse(FourDigits.ToCharArray()[3].ToString()) == 0)
                                    return "ألفين";
                                else
                                {
                                    return "ألفين" + " و " + convertOneDigits(FourDigits.ToCharArray()[3].ToString());
                                }
                            }
                            return "ألفين" + " و " + convertTwoDigits(FourDigits.Substring(2, 2));
                        }
                        else
                        {
                            return "ألفين" + " و " + convertThreeDigits(FourDigits.Substring(1, 3));
                        }
                    }
                case 3:
                    {
                        if (int.Parse(FourDigits.ToCharArray()[1].ToString()) == 0)
                        {
                            if (int.Parse(FourDigits.ToCharArray()[2].ToString()) == 0)
                            {
                                if (int.Parse(FourDigits.ToCharArray()[3].ToString()) == 0)
                                    return convertOneDigits(FourDigits.ToCharArray()[0].ToString()) + " ألاف";
                                else
                                {
                                    return convertOneDigits(FourDigits.ToCharArray()[0].ToString()) + " ألاف" + " و " + convertOneDigits(FourDigits.ToCharArray()[3].ToString());
                                }
                            }
                            return convertOneDigits(FourDigits.ToCharArray()[0].ToString()) + " ألاف" + " و " + convertTwoDigits(FourDigits.Substring(2, 2));
                        }
                        else
                        {
                            return convertOneDigits(FourDigits.ToCharArray()[0].ToString()) + " ألاف" + " و " + convertThreeDigits(FourDigits.Substring(1, 3));
                        }
                    }
                case 4:
                    {
                        goto case 3;
                    }
                case 5:
                    {
                        goto case 3;
                    }
                case 6:
                    {
                        goto case 3;
                    }
                case 7:
                    {
                        goto case 3;
                    }
                case 8:
                    {
                        goto case 3;
                    }
                case 9:
                    {
                        goto case 3;
                    }
                default: return "";
            }
        }
        private string convertFiveDigits(string FiveDigits)
        {
            if (convertThreeDigits(FiveDigits.Substring(2, 3)).Length == 0)
            {
                return convertTwoDigits(FiveDigits.Substring(0, 2)) + " ألف ";
            }
            else
            {
                return convertTwoDigits(FiveDigits.Substring(0, 2)) + " ألفا " + " و " + convertThreeDigits(FiveDigits.Substring(2, 3));
            }
        }
        private string convertSixDigits(string SixDigits)
        {
            if (convertThreeDigits(SixDigits.Substring(2, 3)).Length == 0)
            {
                return convertThreeDigits(SixDigits.Substring(0, 3)) + " ألف ";
            }
            else
            {
                return convertThreeDigits(SixDigits.Substring(0, 3)) + " ألفا " + " و " + convertThreeDigits(SixDigits.Substring(3, 3));
            }
        }

        #endregion

        #region class :: Calculate Date Difference

        //Initializing object and then calling constructor

        //CalculateDateDifference dateDiff = new CalculateDateDifference(dt1, dt2);

        //int totalMonths = dateDiff.Months;

        //int totalDays = dateDiff.Days;

        //int totalYears = dateDiff.Years;

        //Class that will calculate the number of days, months and years between two given dates.

        public class CalculateDateDifference

        {
            /// <summary>

            /// defining Number of days in month; index 0 represents january and 11 represents December

            /// february contain either 28 or 29 days, so here its value is -1

            /// which will be calculate later.

            /// </summary>

            private int[] monthDay = new int[12] { 31, -1, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            /// <summary>

            /// contain from date

            /// </summary>

            private DateTime fromDate;

            /// <summary>

            /// contain To Date

            /// </summary>

            private DateTime toDate;

            /// <summary>

            /// these three variable of integer type for output representation..

            /// </summary>

            private int year;

            private int month;

            private int day;

            //Public type Constructor

            public CalculateDateDifference(DateTime d1, DateTime d2)

            {
                int increment;

                //To Date must be greater

                if (d1 > d2)

                {
                    this.fromDate = d2;

                    this.toDate = d1.AddDays(1);
                }

                else

                {
                    this.fromDate = d1.AddDays(1);

                    this.toDate = d2;
                }

                ///

                /// Day Calculation

                ///

                increment = 0;

                if (this.fromDate.Day > this.toDate.Day)

                {
                    increment = this.monthDay[this.fromDate.Month - 1];
                }

                /// if it is february month

                /// if it's to day is less then from day

                if (increment == -1)

                {
                    if (DateTime.IsLeapYear(this.fromDate.Year))

                    {
                        // leap year february contain 29 days

                        increment = 29;
                    }

                    else

                    {
                        increment = 28;
                    }

                }

                if (increment != 0)

                {
                    day = (this.toDate.Day + increment) - this.fromDate.Day;

                    increment = 1;
                }

                else

                {
                    day = this.toDate.Day - this.fromDate.Day;
                }


                ///

                ///month calculation

                ///

                if ((this.fromDate.Month + increment) > this.toDate.Month)

                {
                    this.month = (this.toDate.Month + 12) - (this.fromDate.Month + increment);

                    increment = 1;
                }

                else

                {
                    this.month = (this.toDate.Month) - (this.fromDate.Month + increment);

                    increment = 0;
                }

                ///

                /// year calculation

                ///

                this.year = this.toDate.Year - (this.fromDate.Year + increment);
            }

            public override string ToString()

            {
                //return base.ToString();

                return this.year + " Year(s), " + this.month + " month(s), " + this.day + " day(s)";
            }
            
            public int Years

            {
                get

                {
                    return this.year;
                }
            }

            public int Months

            {
                get

                {
                    return this.month;
                }
            }
            
            public int Days

            {
                get

                {
                    return this.day;
                }

            }
            
        }

        #endregion

        #region Calculate Contract Fees

        private double CalculateContractFees(double MonthValue, int Years, int Months, int Days, int Type)
        {
            double TotalValue;
            double DaysFees;
            double MonthsFees;
            double YearsFees;

            if (Type == 11041101)
            {
                DaysFees = (Math.Round(((MonthValue / 30) * Days) * .05, 3, MidpointRounding.ToEven));
                MonthsFees = (Math.Round((MonthValue * Months) * .05, 3, MidpointRounding.ToEven));
                YearsFees = (Math.Round(((MonthValue * 12) * Years) * .05, 3, MidpointRounding.ToEven));
            }
            else
            {
                DaysFees = (Math.Round(((MonthValue / 30) * Days), 3, MidpointRounding.ToEven));
                MonthsFees = (Math.Round((MonthValue * Months), 3, MidpointRounding.ToEven));
                YearsFees = (Math.Round(((MonthValue * 12) * Years), 3, MidpointRounding.ToEven));
            }
            TotalValue = DaysFees + MonthsFees + YearsFees;

            return TotalValue;
        }

        #endregion
    }
}