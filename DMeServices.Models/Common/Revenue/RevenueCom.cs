using AutoMapper;
using DMeServices.DAL;
using DMeServices.Models.Revenue;
using DMeServices.Models.ViewModels.Revenue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMeServices.Models.Common.Revenue
{
    public class RevenueCom
    {
        #region Method :: All States

        public static List<State> AllStates()
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<States> States = db.States.ToList();
                List<State> _States = Mapper.Map<List<States>, List<State>>(States);

                return _States;
            }

        }
        #endregion

        #region Method :: All Departments

        public static List<RevDepartment> AllDepartments()
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<RevDepartments> Departments = db.RevDepartments.ToList();
                List<RevDepartment> _Departments = Mapper.Map<List<RevDepartments>, List<RevDepartment>>(Departments);

                return _Departments;
            }

        }
        #endregion

        #region Method :: Department By Employee SiteCd

        public static List<RevDepartment> DepartmentByEmployeeSiteCd(int SiteCd)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<RevDepartments> Department = db.RevDepartments.Where(x => x.Code == SiteCd).ToList();
                List<RevDepartment> _Department = Mapper.Map<List<RevDepartments>, List<RevDepartment>>(Department);

                return _Department;
            }

        }
        #endregion

        #region Method :: All Welayat

        public static List<RevWelyats> AllWelayat()
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<RevWelyat> RevWelyat = db.RevWelyat.ToList();
                List<RevWelyats> _RevWelyat = Mapper.Map<List<RevWelyat>, List<RevWelyats>>(RevWelyat);

                return _RevWelyat;
            }

        }
        #endregion

        #region Method :: All Welayat By Department ID

        public static List<RevWelyats> WelayatByDepartmentID(int DepartmentID)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<RevWelyat> RevWelyat = db.RevWelyat.Where(x => x.SiteCd == DepartmentID).ToList();
                List<RevWelyats> _RevWelyat = Mapper.Map<List<RevWelyat>, List<RevWelyats>>(RevWelyat);

                return _RevWelyat;
            }

        }
        #endregion

        #region Method :: Get Last Payment Code

        public static List<RevMainPayment> LastPaymentCode()
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<RevMainPayments> LastPaymentCode = db.RevMainPayments.Where(x => x.FinancialYear == DateTime.Now.Year).OrderByDescending(x => x.PaymentCode).Take(1).ToList();
                List<RevMainPayment> _LastPaymentCode = Mapper.Map<List<RevMainPayments>, List<RevMainPayment>>(LastPaymentCode);

                return _LastPaymentCode;
            }

        }
        #endregion

        #region Method :: Salalah Welayat

        public static List<RevWelyats> SalalahWelayat()
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<RevWelyat> SalalahWelyat = db.RevWelyat.Where(x => x.SiteCd == 1).ToList();
                List<RevWelyats> _SalalahWelyat = Mapper.Map<List<RevWelyat>, List<RevWelyats>>(SalalahWelyat);

                return _SalalahWelyat;
            }

        }
        #endregion

        #region Method :: Payment Types

        public static List<LookupType> PaymentTypes()
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<LookupTypes> PaymentTypes = db.LookupTypes.Where(x => x.LookupParentId == 6).ToList();
                List<LookupType> _PaymentTypes = Mapper.Map<List<LookupTypes>, List<LookupType>>(PaymentTypes);

                return _PaymentTypes;
            }

        }
        #endregion

        #region Method :: Contract Types

        public static List<LookupType> ContractTypes()
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<LookupTypes> ContractTypes = db.LookupTypes.Where(x => x.LookupParentId == 7).ToList();
                List<LookupType> _ContractTypes = Mapper.Map<List<LookupTypes>, List<LookupType>>(ContractTypes);

                return _ContractTypes;
            }

        }
        #endregion

        #region Method :: Banks

        public static List<RevBank> Banks()
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<RevBanks> Banks = db.RevBanks.ToList();
                List<RevBank> _Banks = Mapper.Map<List<RevBanks>, List<RevBank>>(Banks);

                return _Banks;
            }

        }
        #endregion

        #region Method :: All Financial Items

        public static List<RevFinancialItem> AllFinancialItems()
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<RevFinancialItems> FinancialItems = db.RevFinancialItems.Where(x => x.Code != 11041101 && x.Code != 11083103).ToList();
                List<RevFinancialItem> _FinancialItems = Mapper.Map<List<RevFinancialItems>, List<RevFinancialItem>>(FinancialItems);

                return _FinancialItems;
            }

        }

        public static List<RevFinancialItem> All_FinancialItems()
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<RevFinancialItems> FinancialItems = db.RevFinancialItems.ToList();
                List<RevFinancialItem> _FinancialItems = Mapper.Map<List<RevFinancialItems>, List<RevFinancialItem>>(FinancialItems);

                return _FinancialItems;
            }

        }

        #endregion

        #region Method :: Contracts Financial Item

        public static List<RevFinancialItem> ContractsFinancialItem()
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<RevFinancialItems> FinancialItems = db.RevFinancialItems.Where(x => x.Code == 11041101 || x.Code == 11083103).ToList();
                List<RevFinancialItem> _FinancialItems = Mapper.Map<List<RevFinancialItems>, List<RevFinancialItem>>(FinancialItems);

                return _FinancialItems;
            }

        }
        #endregion

        #region Method :: All Financial SubItems

        public static List<RevFinancialSubItem> AllFinancialSubItems()
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<RevFinancialSubItems> FinancialSubItems = db.RevFinancialSubItems.ToList();
                List<RevFinancialSubItem> _FinancialSubItems = Mapper.Map<List<RevFinancialSubItems>, List<RevFinancialSubItem>>(FinancialSubItems);

                return _FinancialSubItems;
            }

        }
        #endregion

        #region Method :: Financial SubItems By Financial Item ID

        public static List<RevFinancialSubItem> FinancialSubItemsByID(int FinancialId)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<RevFinancialSubItems> FinancialSubItems = db.RevFinancialSubItems.Where(x => x.FinancialItemId == FinancialId).OrderBy(x=> x.Code).ToList();
                List<RevFinancialSubItem> _FinancialSubItems = Mapper.Map<List<RevFinancialSubItems>, List<RevFinancialSubItem>>(FinancialSubItems);

                return _FinancialSubItems;
            }

        }
        #endregion

        #region Method :: Save Main Payments 

        public static long SaveMainPayments(RevenueViewModel oModel)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                RevMainPayments _RevMainPayments = new RevMainPayments();
                try
                {
                    int Code = 0;
                    if (LastPaymentCode().Count > 0)
                    {
                        foreach (var item in LastPaymentCode())
                        {
                            Code = int.Parse(item.PaymentCode.ToString()) + 1;
                        }
                    }
                    else
                    {
                        _RevMainPayments.PaymentCode = 1;
                    }
                    
                    _RevMainPayments = Mapper.Map<RevMainPayment, RevMainPayments>(oModel.RevMainPayment);
                    _RevMainPayments.PaymentCode = Code;
                    _RevMainPayments.FinancialYear = DateTime.Now.Year;
                    _RevMainPayments.InsertedBy = oModel.oEmployeeInfo.EMP_NO.ToString();
                    _RevMainPayments.UserCode = oModel.oEmployeeInfo.EMP_NO;
                    _RevMainPayments.InsertedOn = DateTime.Now;
                    _RevMainPayments.LocationCode = 0;
                    _RevMainPayments.SiteCd = oModel.RevMainPayment.DeptCode;
                    _RevMainPayments.PaymentDate = DateTime.Now;
                    if(oModel.RevMainPayment.PaymentType=="22")
                    {
                        _RevMainPayments.PaymentType = "D";
                        _RevMainPayments.PaymentCheqDate = null;
                    }
                    else
                    {
                        _RevMainPayments.PaymentType = "Q";
                        _RevMainPayments.PaymentCheqDate = DateTime.Parse(DateTime.Parse(oModel.RevMainPayment.PaymentCheqDate.ToString()).ToShortDateString());
                    }
                    

                    db.RevMainPayments.Add(_RevMainPayments);
                    db.SaveChanges();
                    
                    return (long)_RevMainPayments.PaymentCode;

                }

                catch (Exception ex)
                {
                    throw ex;
                }

            }

        }

        #endregion
        
        #region Method :: Update Main Payments 

        public static long UpdateMainPayments(RevenueViewModel oModel)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                RevMainPayments _RevMainPayments = new RevMainPayments();
                try
                {

                    _RevMainPayments = Mapper.Map<RevMainPayment, RevMainPayments>(oModel.RevMainPayment);

                    long Rev_ID = (long)_RevMainPayments.Id;
                    var RevMainPaymentData = db.RevMainPayments.Where(x => x.Id == Rev_ID).FirstOrDefault();
                    
                    RevMainPaymentData.FinancialItemId = oModel.RevMainPayment.FinancialItemId;
                    RevMainPaymentData.FinancialSubItemId = oModel.RevMainPayment.FinancialSubItemId;
                    RevMainPaymentData.PayerName = oModel.RevMainPayment.PayerName;
                    if (oModel.RevMainPayment.PaymentType == "22")
                    {
                        RevMainPaymentData.PaymentType = "D";
                        RevMainPaymentData.PaymentCheqDate = null;
                    }
                    else
                    {
                        RevMainPaymentData.PaymentType = "Q";
                        RevMainPaymentData.PaymentCheqDate = DateTime.Parse(DateTime.Parse(oModel.RevMainPayment.PaymentCheqDate.ToString()).ToShortDateString());
                    }
                    RevMainPaymentData.PaymentCheqNo = oModel.RevMainPayment.PaymentCheqNo;
                    RevMainPaymentData.PaymentBankCode = oModel.RevMainPayment.PaymentBankCode;
                    RevMainPaymentData.PaymentCheqDate = oModel.RevMainPayment.PaymentCheqDate;
                    RevMainPaymentData.PaymentAmount = oModel.RevMainPayment.PaymentAmount;
                    RevMainPaymentData.PaymentAmountInWords = oModel.RevMainPayment.PaymentAmountInWords;
                    //RevMainPaymentData.PaymentDate = oModel.RevMainPayment.PaymentDate;

                    RevMainPaymentData.UpdatedBy = oModel.oEmployeeInfo.EMP_NO.ToString();
                    RevMainPaymentData.UpdatedOn = DateTime.Now;
                    RevMainPaymentData.PaymentCheqName = oModel.RevMainPayment.PaymentCheqName;
                    RevMainPaymentData.CreditCardNo = oModel.RevMainPayment.CreditCardNo;

                    db.SaveChanges();

                    return (long)_RevMainPayments.PaymentCode;

                }

                catch (Exception ex)
                {
                    throw ex;
                }

            }

        }

        #endregion

        #region Method :: Revenue Data

        public static List<RevMainPayment> RevenueData()
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<RevMainPayments> RevenueData = db.RevMainPayments.OrderByDescending(x => x.Id).ToList();
                List<RevMainPayment> _RevenueData = Mapper.Map<List<RevMainPayments>, List<RevMainPayment>>(RevenueData);

                return _RevenueData;
            }

        }
        #endregion

        #region Method :: Revenue Data By Code

        public static List<RevMainPayment> RevenueDataByCode(int Payment_Code,int Financial_Year)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<RevMainPayments> RevenueData = db.RevMainPayments.Where(x => x.PaymentCode == Payment_Code && x.FinancialYear == Financial_Year).ToList();
                List<RevMainPayment> _RevenueData = Mapper.Map<List<RevMainPayments>, List<RevMainPayment>>(RevenueData);

                return _RevenueData;
            }

        }
        #endregion

        #region Method :: Revenue Data By Id

        public static RevMainPayment RevenueDataById(int _Id)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                RevMainPayments RevenueDataById = db.RevMainPayments.Where(x => x.Id == _Id).SingleOrDefault();
                RevMainPayment _RevenueDataById = Mapper.Map<RevMainPayments, RevMainPayment>(RevenueDataById);

                return _RevenueDataById;
            }

        }
        #endregion
        
    }
}
