using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using EntityMap;

using MoneyCare;
using MoneyCare.Model;
using MoneyCare.Repository;
using BrightIdeasSoftware;
using System.IO;

namespace MoneyCare.View
{

    public enum FormMode
    {
        AddNew,Edit
    }


    public partial class MainUI : Form
    {
        private string view;
        private bool isFirstLoad = false;
       
        private TransactionRepository transactionRepository;
        private AccountRepository accountRepository;
        private CategoryRepository categoryRepository;
        private ReminderRepository reminderRepository;
        private ChartRepository chartRepository;
        private SettingRepository settingRepository;
        private int successImport;
        private int failedImport;
        private StringBuilder errorData;

        private const string CHART_CASH_AND_BANK = "Kas, Bank, dan Kartu Kredit";
        private const string CHART_CREDIT_CARD = "Kartu Kredit";
        private const string CHART_INCOME = "Pendapatan";
        private const string CHART_EXPENSE = "Pengeluaran";
        private const string CHART_ACCOUNT_PAYABLE = "Hutang";
        private const string CHART_ACCOUNT_RECEIVABLE = "Piutang";
        private const string CHART_FINANCIAL_ADVISOR = "Penasihat Keuangan";
               
        private const string VIEW_TRANSACTION="Transaction";
        private const string VIEW_ACCOUNT="Account";
        private const string VIEW_CATEGORY="Category";
        private const string VIEW_BUDGET="Budget";
        private const string VIEW_REMINDER="Reminder";

        private const string TRANSACTION_ALL = "Semua";
        private const string TRANSACTION_INCOME="Pendapatan";
        private const string TRANSACTION_EXPENSE="Pengeluaran";
        private const string TRANSACTION_TRANSFER="Transfer Bank";
        private const string TRANSACTION_WITHDRAWL="Penarikan Tunai";
        private const string TRANSACTION_DEPOSIT = "Setor Tunai";
    
        private const string ACCOUNT_ALL = "Semua";
        private const string ACCOUNT_CASH="Kas";
        private const string ACCOUNT_BANK="Bank";
        private const string ACCOUNT_CREDIT_CARD = "Kartu Kredit";

        private const string CATAGORY_ALL="Semua";
        private const string CATAGORY_INCOME="Pendapatan";
        private const string CATAGORY_EXPENSE="Pengeluaran";

        private const string BUDGET_ALL = "Semua";

        private const string REMINDER_ALL="Semua";
        private const string REMINDER_PAID="Sudah di Bayar";
        private const string REMINDER_UNPAID="Belum di Bayar";

        private const string IMPORT_INCOME = "INCOME";
        private const string IMPORT_EXPENSE = "EXPENSE";
        private const string IMPORT_TRANSFER = "TRANSFER";
        private const string IMPORT_WITHDRAWL = "WITHDRAWL";
        private const string IMPORT_DEPOSIT = "DEPOSIT";
        private const string IMPORT_PAYMENT = "PAYMENT";



        public MainUI()
        {

            transactionRepository = new TransactionRepository();
            categoryRepository = new CategoryRepository();
            accountRepository = new AccountRepository();
            reminderRepository = new ReminderRepository();
            chartRepository = new ChartRepository();
            settingRepository = new SettingRepository();

            InitializeComponent();
        }




        public void FilterTransaction(List<Model.Transaction> transactions)
        {
            int month = cboMonth.SelectedIndex + 1;
            int year = int.Parse(cboYear.Text);

            lvwHistory.Items.Clear();

            foreach (Model.Transaction transaction in transactions)
            {
                RenderTransaction(transaction);
            }

        }

        public int CurrentMonth
        {
            get { return cboMonth.SelectedIndex + 1; }
        }

        public int CurrentYear
        {
            get { return int.Parse(cboYear.Text); }
        }


        public string LvwHistoryTransactionType
        {
            get { return lvwHistory.FocusedItem.SubItems[4].Text; }
        }

        public string CboFilterText
        {
            get { return cboFilter.Text; }
        }

        public string CboChart
        {
            get { return cboChart.Text; }
        }


        public void DisableEditDelete()
        {
            tsbEdit.Enabled = false;
            tsbDelete.Enabled = false; 
        }

        public void EnableEditDelete()
        {
            tsbEdit.Enabled = true;
            tsbDelete.Enabled = true;
        }

        public void DisableAction()
        {
            tspSeparator3.Visible = false;
            tsbAction.Visible = false;
        }



        private void ViewFinancialCheckup()
        {            
            int month=this.CurrentMonth;
            int year=this.CurrentYear;

            //Setting

            string status = Store.settings.Where(s => s.Key == Store.SETTING_STATUS).Single().Value;

            string ea=Store.settings.Where(s => s.Key == Store.SETTING_EMERGENCY_ACCOUNT).Single().Value;
            string sa = Store.settings.Where(s => s.Key == Store.SETTING_SAVING_ACCOUNT).Single().Value;
            string ia = Store.settings.Where(s => s.Key == Store.SETTING_INVESTMENT_ACCOUNT).Single().Value;
            string ae = Store.settings.Where(s => s.Key == Store.SETTING_AVERAGE_EXPENSE).Single().Value;

        
            if (ea == "" || sa == "" || ia == "" || ae=="")
            {
                MessageBox.Show("Isi terlabih dahulu rekening terkait","Perhatian",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                SettingUI frmSetting = new SettingUI();
                frmSetting.SelectTab(1);
                frmSetting.Show();
            }
            else
            {

                Guid emergencyFundAccount = new Guid(ea);
                Guid savingAccount = new Guid(sa);
                Guid investmentAccount = new Guid(ia);

                decimal emergencyFundBalance = accountRepository.GetBalance(emergencyFundAccount);
                decimal savingBalance = accountRepository.GetBalance(savingAccount);
                decimal investmentBalance = accountRepository.GetBalance(investmentAccount);
                decimal averageExpense = decimal.Parse(ae);
                decimal mustHaveBalance = 0;
                int liquidityRatioIdeal = 0;

                //Emergency Fund

                switch (status)
                {
                    case "Single" :
                        liquidityRatioIdeal = 6;
                        break;
                    case "Menikah" :
                        liquidityRatioIdeal = 6;
                        break;
                    case "Menikah (1 Anak)" :
                        liquidityRatioIdeal = 9;
                        break;
                    case "Menikah (2 Anak atau lebih)":
                        liquidityRatioIdeal = 12;
                        break;
                }

                mustHaveBalance = liquidityRatioIdeal * averageExpense;
             
               
                if (mustHaveBalance > 0)
                {
                    lblEFBank.Text = accountRepository.GetById(emergencyFundAccount).Name;
                    lblEFAvailable.Text = emergencyFundBalance.ToString("N0").Replace(",", ".");
                    lblEFMustHave.Text = mustHaveBalance.ToString("N0").Replace(",", ".");
                    decimal emergencyFundPercentage = (emergencyFundBalance / mustHaveBalance) * 100;
                    lblEFPercentage.Text = emergencyFundPercentage.ToString("N1") + " %";

                    int pnlEF1Width = Convert.ToInt32(pnlEF2.Width * (emergencyFundPercentage / 100));

                    if (pnlEF1Width <= pnlEF2.Width)
                    {
                        pnlEF1.Width = pnlEF1Width;
                    }
                    else
                    {
                        pnlEF1.Width = pnlEF2.Width;
                    }
                }
                
                decimal totalExpense = transactionRepository.GetTotalTransaction(CategoryType.Expense, month, year);
                decimal totalIncome = transactionRepository.GetTotalTransaction(CategoryType.Income, month, year);

                lblIncome.Text = totalIncome.ToString("N0").Replace(",", ".");
                lblExpense.Text = totalExpense.ToString("N0").Replace(",", ".");
                
                if (totalIncome > totalExpense)
                {
                    lblCashFlowStatus.ForeColor = Color.Black;
                    lblCashFlowStatus.Text = "SURPLUS";
                    lblCashFlowBalance.Text = " + " + (totalIncome - totalExpense).ToString("N0").Replace(",", ".");
                }
                else if (totalIncome < totalExpense)
                {
                    lblCashFlowStatus.ForeColor = Color.Red;
                    lblCashFlowStatus.Text = "DEFISIT";
                    lblCashFlowBalance.Text = (totalIncome - totalExpense).ToString("N0").Replace(",", ".");
                }
                else if (totalIncome == totalExpense)
                {
                    lblCashFlowStatus.ForeColor = Color.Black;
                    lblCashFlowStatus.Text = "NETRAL";
                    lblCashFlowBalance.Text = "0";
                }

                //Liquidity Ratio

                if (averageExpense > 0)
                {
                    decimal liquidityRatio = emergencyFundBalance / averageExpense;

                    lblLiquidityRatio.Text = liquidityRatio.ToString("N1") + " Bulan";
                    lblLiquidityRatioIdeal.Text = "Ideal : " + liquidityRatioIdeal.ToString() + " Bulan";

                    if (status=="Single" && liquidityRatio >= 6)
                    {
                        lblLiquidityStatus.ForeColor = Color.Black;
                        lblLiquidityStatus.Text = "BAIK";
                    }
                    else if (status == "Menikah" && liquidityRatio >= 6)
                    {
                        lblLiquidityStatus.ForeColor = Color.Black;
                        lblLiquidityStatus.Text = "BAIK";
                    }
                    else if (status == "Menikah (1 Anak)" && liquidityRatio >= 9)
                    {
                        lblLiquidityStatus.ForeColor = Color.Black;
                        lblLiquidityStatus.Text = "BAIK";
                    }
                    else if (status == "Menikah (2 Anak atau lebih)" && liquidityRatio >= 12)
                    {
                        lblLiquidityStatus.ForeColor = Color.Black;
                        lblLiquidityStatus.Text = "BAIK";
                    }
                    else
                    {
                        lblLiquidityStatus.ForeColor = Color.Red;
                        lblLiquidityStatus.Text = "BURUK";
                    }
                }


                //Debt Payment Ratio

                decimal monthlyDebt = transactionRepository.GetMonthlyDebt(month, year);

                if (totalIncome > 0 && monthlyDebt > 0)
                {
                    //decimal monthlyDebt = transactionRepository.GetMonthlyDebt(month, year);
                    decimal debtRatio = (monthlyDebt / totalIncome) * 100;

                    int pnlDebtRatio1Width = Convert.ToInt32(pnlDebtRatio2.Width * (debtRatio / 100));

                    if (pnlDebtRatio1Width <= pnlDebtRatio2.Width)
                    {
                        pnlDebtRatio1.Width = pnlDebtRatio1Width;
                    }
                    else
                    {
                        pnlDebtRatio1.Width = pnlDebtRatio2.Width;
                    }

                    lblDebtRatio.Text = debtRatio.ToString("N1") + " %";

                    if (debtRatio > 30)
                    {
                        lblDebtStatus.Text = "BURUK";
                        lblDebtStatus.ForeColor = Color.Red;

                    }
                    else
                    {
                        lblDebtStatus.Text = "BAIK";
                        lblDebtStatus.ForeColor = Color.Black;
                    }
                }
                else
                {
                    pnlDebtRatio1.Width = 0;
                    lblDebtRatio.Text = "0 %";
                    lblDebtStatus.Text = "N/A";
                }


                //Saving Ratio

                if (totalIncome > 0)
                {
                    decimal savingRatio = (savingBalance / totalIncome) * 100;
                    lblSavingRatio.Text = savingRatio.ToString("N1") + " %";

                    int pnlSavingRatio1Width = Convert.ToInt32(pnlSavingRatio2.Width * (savingRatio / 100));

                    if (pnlSavingRatio1Width <= pnlSavingRatio2.Width)
                    {
                        pnlSavingRatio1.Width = pnlSavingRatio1Width;
                    }
                    else
                    {
                        pnlSavingRatio1.Width = pnlSavingRatio2.Width;
                    }


                    if (savingRatio >= 10)
                    {
                        lblSavingStatus.ForeColor = Color.Black;
                        lblSavingStatus.Text = "BAIK";
                    }
                    else
                    {
                        lblSavingStatus.ForeColor = Color.Red;
                        lblSavingStatus.Text = "BURUK";
                    }

                }
                else
                {
                    pnlSavingRatio1.Width = 0;
                    lblSavingRatio.Text = "0 %";
                    lblSavingStatus.Text = "N/A";
                }


                //Investment Ratio

                decimal cashAndBankBalance = accountRepository.GetCurrentCashAndBank();

                if (cashAndBankBalance > 0)
                {
                    decimal investmentRatio = (investmentBalance / cashAndBankBalance) * 100;

                    lblInvestmentRatio.Text = investmentRatio.ToString("N1") + " %";

                    int pnlInvestmentRatio1Width = Convert.ToInt32(pnlInvestmentRatio2.Width * (investmentRatio / 100));

                    if (pnlInvestmentRatio1Width <= pnlInvestmentRatio2.Width)
                    {
                        pnlInvestmentRatio1.Width = pnlInvestmentRatio1Width;
                    }
                    else
                    {
                        pnlInvestmentRatio1.Width = pnlInvestmentRatio2.Width;
                    }

                    if (investmentRatio >= 10)
                    {
                        lblInvestmentStatus.Text = "BAIK";
                        lblInvestmentStatus.ForeColor = Color.Black;
                    }
                    else
                    {
                        lblInvestmentStatus.Text = "BURUK";
                        lblInvestmentStatus.ForeColor = Color.Red;
                    }
                }
                else
                {
                    pnlInvestmentRatio1.Width = 0;
                    lblInvestmentRatio.Text = "0 %";
                    lblInvestmentStatus.Text = "N/A";
                }
            }
        }


        private void RenderTransaction(Model.Transaction transaction)
        {
            ListViewItem item = new ListViewItem(transaction.ID.ToString());

            item.SubItems.Add(transaction.Date.ToString("dd/MM/yyyy"));

            if (transaction.Notes == string.Empty)
            {
                item.SubItems.Add(transaction.Description);
            }
            else
            {
                item.SubItems.Add(transaction.Description + " ( " + transaction.Notes + " )");
            }
         
            item.SubItems.Add(transaction.Amount.ToString("N0").Replace(",","."));
            item.SubItems.Add(transaction.Type);

            lvwHistory.Items.Add(item);

        }


        private void RenderCategory(Category category)
        {
            ListViewItem item = new ListViewItem(category.ID.ToString());

            item.SubItems.Add(category.Name);
            item.SubItems.Add(category.Type);
            item.SubItems.Add(category.Group);
            item.SubItems.Add(category.Budget.ToString("N0").Replace(",", "."));

            lvwCategory.Items.Add(item);
        }


      

        private void RenderAccount(Account account)
        {
            ListViewItem item = new ListViewItem(account.ID.ToString());

            item.SubItems.Add(account.Name);
            item.SubItems.Add(account.Type);
            item.SubItems.Add(account.Balance.ToString("N0").Replace(",", "."));
            lvwAccount.Items.Add(item);
        }


        private void RenderReminder(Reminder reminder)
        {
            ListViewItem item = new ListViewItem(reminder.ID.ToString());

            item.SubItems.Add(reminder.Description);
            item.SubItems.Add(reminder.DueDate.ToShortDateString());
            item.SubItems.Add(reminder.Amount.ToString("N0").Replace(",", "."));

            DateTime d1 = DateTime.Now;
            DateTime d2 = reminder.DueDate;

            TimeSpan t = d2 - d1;
            double totalDay = t.TotalDays;
            string d = totalDay.ToString("N0");

            if (d == "0" )
            {
                item.SubItems.Add("Jatuh tempo hari ini");
            }
            else if (totalDay <= 0)
            {
                item.SubItems.Add("Lewat jatuh tempo " + d.Substring(1) + " hari");
            }
            else
            {
                item.SubItems.Add("Jatuh tempo " + d + " hari lagi");
            }
          
            lvwReminder.Items.Add(item);
        }



        private void LoadFilterMenu(string view)
        {
            cboFilter.Items.Clear();

            switch (view)
            {
                case VIEW_TRANSACTION:
                   
                                    
                    cboFilter.Items.Add(TRANSACTION_ALL);
                    cboFilter.Items.Add(TRANSACTION_INCOME);
                    cboFilter.Items.Add(TRANSACTION_EXPENSE);
                    cboFilter.Items.Add(TRANSACTION_TRANSFER);
                    cboFilter.Items.Add(TRANSACTION_WITHDRAWL);
                    cboFilter.Items.Add(TRANSACTION_DEPOSIT);
               
                    break;

                case VIEW_ACCOUNT :

                    cboFilter.Items.Add(ACCOUNT_ALL);
                    cboFilter.Items.Add(ACCOUNT_CASH);
                    cboFilter.Items.Add(ACCOUNT_BANK);
                    cboFilter.Items.Add(ACCOUNT_CREDIT_CARD);
                   
                    break;

                case VIEW_CATEGORY:

                    cboFilter.Items.Add(CATAGORY_ALL);
                    cboFilter.Items.Add(CATAGORY_INCOME);
                    cboFilter.Items.Add(CATAGORY_EXPENSE);

                    break;

                case VIEW_BUDGET :
                    
                    cboFilter.Items.Add(BUDGET_ALL);
                    break;

                case VIEW_REMINDER :

                     cboFilter.Items.Add(REMINDER_ALL);
                     cboFilter.Items.Add(REMINDER_UNPAID);
                     cboFilter.Items.Add(REMINDER_PAID);

                    break;
                   
            }
            
            cboFilter.Text = TRANSACTION_ALL;
        }


        private void LoadChartMenu()
        {
            cboChart.Items.Add(CHART_CASH_AND_BANK);
            cboChart.Items.Add(CHART_CREDIT_CARD);
            cboChart.Items.Add(CHART_INCOME);
            cboChart.Items.Add(CHART_EXPENSE);
            cboChart.Items.Add(CHART_ACCOUNT_PAYABLE);
            cboChart.Items.Add(CHART_ACCOUNT_RECEIVABLE);
            cboChart.Items.Add(CHART_FINANCIAL_ADVISOR);


            cboChart.Text = CHART_CASH_AND_BANK;
        }


        public string ConvertMonthToString(int month)
        {
            string monthName = string.Empty;

            switch (month)
            {
                case 1: monthName = "Januari";
                    break;
                case 2: monthName = "Februari";
                    break;
                case 3: monthName = "Maret";
                    break;
                case 4: monthName = "April";
                    break;
                case 5: monthName = "Mei";
                    break;
                case 6: monthName = "Juni";
                    break;
                case 7: monthName = "Juli";
                    break;
                case 8: monthName = "Agustus";
                    break;
                case 9: monthName = "September";
                    break;
                case 10: monthName = "Oktober";
                    break;
                case 11: monthName = "November";
                    break;
                case 12: monthName = "Desember";
                    break;
            }

            return monthName;

        }

        private void LoadMonth()
        {
            cboMonth.Items.Add("Januari");
            cboMonth.Items.Add("Februari");
            cboMonth.Items.Add("Maret");
            cboMonth.Items.Add("April");
            cboMonth.Items.Add("Mei");
            cboMonth.Items.Add("Juni");
            cboMonth.Items.Add("Juli");
            cboMonth.Items.Add("Agustus");
            cboMonth.Items.Add("September");
            cboMonth.Items.Add("Oktober");
            cboMonth.Items.Add("November");
            cboMonth.Items.Add("Desember");

            string month=ConvertMonthToString(DateTime.Now.Month);
            cboMonth.Text = month;
       
        }



        private decimal PlotData(List<string> name, List<decimal> amount, Dictionary<string, decimal> data)
        {
            decimal totalAmount = 0;

            foreach (KeyValuePair<string, decimal> pair in data)
            {
                name.Add(pair.Key);
                amount.Add(pair.Value);

                totalAmount = totalAmount + pair.Value;
            }
            return totalAmount;
        }




        public void DrawChart(string chartName)
        {

            List<string> name = new List<string>();
            List<decimal> amount = new List<decimal>();

            Dictionary<string, decimal> data = new Dictionary<string, decimal>();
            lblChartTitle.Text = cboChart.Text.ToUpper() + " " + cboMonth.Text.ToUpper() + " " + cboYear.Text;


            switch (chartName)
            {
                case CHART_CASH_AND_BANK:

                    pnlFinancialCheckup.Visible = false;

                    decimal totalCurrentCashBank = PlotData(name, amount, chartRepository.GetCurrentCashAndBank());

                    lblChartTitle.Text = "SALDO " + cboChart.Text.ToUpper() + " " + ConvertMonthToString(DateTime.Now.Month).ToUpper() + " " + cboYear.Text;
                    lblChartTotal.Text = totalCurrentCashBank.ToString("N0").Replace(",", ".");
                    ShowHideChartTitle();

                    break;

                case CHART_CREDIT_CARD:

                    pnlFinancialCheckup.Visible = false;

                    decimal totalMonthlyCreditCard = PlotData(name, amount, chartRepository.GetMonthlyCreditCard(this.CurrentMonth,this.CurrentYear));
                   
                    lblChartTitle.Text = "SALDO PEMAKAIAN KARTU KREDIT " + cboMonth.Text.ToUpper() + " " + cboYear.Text;
                    lblChartTotal.Text = totalMonthlyCreditCard.ToString("N0").Replace(",", ".");
                    ShowHideChartTitle();

                    break;

                case CHART_INCOME:

                    pnlFinancialCheckup.Visible = false;

                    decimal totalMonthlyIncome = PlotData(name, amount, chartRepository.GetMonthlyIncome(this.CurrentMonth,this.CurrentYear));

                    lblChartTotal.Text = totalMonthlyIncome.ToString("N0").Replace(",", ".");
                    ShowHideChartTitle();

                    break;

                case CHART_EXPENSE:

                    pnlFinancialCheckup.Visible = false;
                    decimal totalMonthlyExpense =  PlotData(name, amount, chartRepository.GetMonthlyExpense(this.CurrentMonth,this.CurrentYear));

                    lblChartTotal.Text = totalMonthlyExpense.ToString("N0").Replace(",", ".");
                    ShowHideChartTitle();

                    break;


                case CHART_ACCOUNT_PAYABLE:

                    pnlFinancialCheckup.Visible = false;
                    decimal totalMonthlyAccountPayable = PlotData(name, amount, chartRepository.GetAccountPayableBalance());
                                       
                    lblChartTitle.Text = "SALDO " + cboChart.Text.ToUpper() + " " + cboMonth.Text.ToUpper() + " " + cboYear.Text;
                    lblChartTotal.Text = totalMonthlyAccountPayable.ToString("N0").Replace(",", ".");

                    ShowHideChartTitle();

                    break;

                case CHART_ACCOUNT_RECEIVABLE:

                    pnlFinancialCheckup.Visible = false;
                    decimal totalMonthlyAccountReceivable = PlotData(name, amount, chartRepository.GetAccountReceivableBalance());

                    lblChartTitle.Text = "SALDO " + cboChart.Text.ToUpper() + " " + cboMonth.Text.ToUpper() + " " + cboYear.Text;
                    lblChartTotal.Text = totalMonthlyAccountReceivable.ToString("N0").Replace(",", ".");

                    ShowHideChartTitle();
                    
                    break;


                case CHART_FINANCIAL_ADVISOR:

                    ViewFinancialCheckup();
                    pnlFinancialCheckup.Visible = true;

                    break;
            }

            chart1.Series["Series1"].Points.DataBindXY(name, amount);
            chart1.Series["Series1"].Label = "#PERCENT";
            chart1.Series["Series1"].ToolTip = "#VALX ( #PERCENT )";
            chart1.Series["Series1"].LegendText = "#AXISLABEL : #VALY{N0}";
            chart1.Series["Series1"].ChartType = SeriesChartType.Doughnut;

            chart1.Series["Series1"]["PieLabelStyle"] = "Disabled";
            chart1.Series["Series1"]["DoughnutRadius"] = "60";
            chart1.Series["Series1"]["PieDrawingStyle"] = "Concave";


            chart1.Visible = true;


        }





        private void ShowHideChartTitle()
        {
            if (lblChartTotal.Text == "0")
            {
                lblChartTitle.Visible = false;
                lblChartTotal.Visible = false;
            }
            else
            {
                lblChartTitle.Visible = true;
                lblChartTotal.Visible = true;

            }
        }



       


        private void cbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isFirstLoad)
            {
                tsmExportByMonth.Text = "Bulan " + cboMonth.Text;
                
                LoadTransactionByType(cboFilter.Text);
                LoadBudgetByType(cboFilter.Text);
                LoadReminderByStatus(cboFilter.Text);
            }

            DrawChart(cboChart.Text);

        }

      
        private void lvwCashFlow_DoubleClick(object sender, EventArgs e)
        {
            if (lvwHistory.Items.Count != 0)
            {
                TransactionUI frmTransaction = new TransactionUI(new Guid(lvwHistory.FocusedItem.Text),this);
                frmTransaction.ShowDialog();
            }
        }



        private void Main_Load(object sender, EventArgs e)
        {
            
            string name = Store.settings.Where(s => s.Key == "NAME").Single().Value;

            if (name == string.Empty)
            {
                this.Text = "Money Care";
            }
            else
            {
                this.Text = "Money Care - " + name;
            }

            this.isFirstLoad = true;

            tspSeparator2.Visible = false;

            this.view = "Transaction";
                     

            int currentYear = DateTime.Now.Year;

            cboYear.Items.Add(currentYear - 5);
            cboYear.Items.Add(currentYear - 4);
            cboYear.Items.Add(currentYear - 3);
            cboYear.Items.Add(currentYear - 2);
            cboYear.Items.Add(currentYear - 1);
            cboYear.Items.Add(currentYear);
            cboYear.Items.Add(currentYear + 1);
            cboYear.Items.Add(currentYear + 2);
            cboYear.Items.Add(currentYear + 3);
            cboYear.Items.Add(currentYear + 4);
            cboYear.Items.Add(currentYear + 5);

            cboYear.Text = currentYear.ToString();


            LoadMonth();
            LoadFilterMenu(this.view);

            LoadChartMenu();

            tsmExportByMonth.Text = "Bulan " + cboMonth.Text;
            tsmExportByYear.Text = "Tahun " + this.CurrentYear;

         

            
           
            
        }

      
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.view == VIEW_TRANSACTION)
            {
                TransactionUI frmTransaction = new TransactionUI(this);
                frmTransaction.Show();
            }
            else if (this.view == VIEW_ACCOUNT)
            {
                AccountUI frmAccount = new AccountUI(this);
                frmAccount.Show();
            }
            else if (this.view == VIEW_CATEGORY)
            {
                CategoryUI frmCategory = new CategoryUI(this);
                frmCategory.Show();
            }
            else if (this.view == VIEW_REMINDER)
            {
                ReminderUI frmReminder = new ReminderUI(this);
                frmReminder.Show();
            }
            else if (this.view == VIEW_BUDGET)
            {
                BudgetUI frmBudget = new BudgetUI(this);
                frmBudget.Show();
            }
        }


        private void tsbHistory_Click(object sender, EventArgs e)
        {
            this.view = VIEW_TRANSACTION;

            btnAdd.ToolTipText = "Tambah Transaksi";
            lblCaption.Text = "Transaksi";
            btnAdd.Visible = true;

            if (tsmByMonthYear.Checked)
            {
                cboYear.Visible = true;
            }
            else
            {
                cboYear.Visible = false;
            }
            
            tsbHistory.Checked = true;
            tsbAccount.Checked = false;
            tsbCategory.Checked = false;
            tsbBudget.Checked = false;
            tsbReminder.Checked = false;
            tsbFilter.Visible = true;
            cboMonth.Visible = true;
            tsbAction.Visible = false;

            tspSeparator1.Visible = true;
            tspSeparator2.Visible = false;
            tspSeparator3.Visible = true;

            lvwHistory.Visible = true;
          
            lvwCategory.Visible = false;
            olvBudget.Visible = false;           
            lvwAccount.Visible = false;
            lvwReminder.Visible = false;

            tsmFilterSeparator.Visible = true;
            tsmAdvancedFilter.Visible = true;
                      
            LoadFilterMenu(this.view);

        }

        private void btnAccount_Click(object sender, EventArgs e)
        {
            this.view = VIEW_ACCOUNT;
           
            if (lvwAccount.Items.Count == 0) lvwAccount.ContextMenuStrip = null;
                        
            lblCaption.Text = "Rekening";
            btnAdd.ToolTipText = "Tambah Rekening";
          
            btnAdd.Visible = true;

            tsbHistory.Checked = false;
            tsbAccount.Checked = true;
            tsbCategory.Checked = false;
            tsbBudget.Checked = false;
            tsbReminder.Checked = false;
            tsbFilter.Visible = false;
            cboMonth.Visible = false;
            cboYear.Visible = false;
            tsbAction.Visible = false;

            tspSeparator1.Visible = false;
            tspSeparator2.Visible = false;
            tspSeparator3.Visible = false;

            lvwAccount.Visible = true;
            lvwHistory.Visible = false;
            lvwCategory.Visible = false;
            olvBudget.Visible = false;
            lvwReminder.Visible = false;

           
            LoadFilterMenu(this.view);
         
     
        }

        private void tsbCategory_Click(object sender, EventArgs e)
        {
            this.view = VIEW_CATEGORY;
            
            btnAdd.ToolTipText = "Tambah Kategori";
            lblCaption.Text = "Kategori";

            btnAdd.Visible = true;

            tspSeparator1.Visible = false;
            tspSeparator2.Visible = false;
            tspSeparator3.Visible = false;
           
            tsbHistory.Checked = false;
            tsbAccount.Checked = false;
            tsbCategory.Checked = true;
            tsbBudget.Checked = false;
            tsbReminder.Checked = false;
            tsbFilter.Visible = false;
            cboMonth.Visible = false;
            cboYear.Visible = false;
            tsbAction.Visible = false;

            lvwCategory.Visible = true;
            lvwHistory.Visible = false;
            lvwAccount.Visible = false;
            olvBudget.Visible = false;
            lvwReminder.Visible = false;

         
            LoadFilterMenu(this.view);
        }


        private void tsbBudget_Click(object sender, EventArgs e)
        {
            this.view = VIEW_BUDGET;

            lblCaption.Text = "Anggaran";

            tsbHistory.Checked = false;
            tsbAccount.Checked = false;
            tsbCategory.Checked = false;
            tsbBudget.Checked = true;
            tsbReminder.Checked = false;

            tsbFilter.Visible = true;
            cboMonth.Visible = true;
            tsbAction.Visible = false;

            tspSeparator1.Visible = true;
            tspSeparator2.Visible = false;
            tspSeparator3.Visible = true;

            olvBudget.Visible = true;
            lvwHistory.Visible = false;
            lvwAccount.Visible = false;
            lvwCategory.Visible = false;
            lvwReminder.Visible = false;

            tsmFilterSeparator.Visible = false;
            tsmAdvancedFilter.Visible = false;

            LoadFilterMenu(this.view);

        }

      

        private void tsbReminder_Click(object sender, EventArgs e)
        {
            this.view = VIEW_REMINDER;

            btnAdd.ToolTipText = "Tambah Reminder";
            lblCaption.Text = "Reminder";

            btnAdd.Visible = true;

            tsbHistory.Checked = false;
            tsbAccount.Checked = false;
            tsbCategory.Checked = false;
            tsbBudget.Checked = false;
            tsbReminder.Checked = true;
            tsbFilter.Visible = true;
            cboMonth.Visible = true;
            tsbAction.Visible = false;

            tspSeparator1.Visible = true;
            tspSeparator2.Visible = false;
            tspSeparator3.Visible = true;

            lvwReminder.Visible = true;
            olvBudget.Visible = false;
            lvwHistory.Visible = false;
            lvwAccount.Visible = false;
            lvwCategory.Visible = false;

            lvwReminder.Visible = true;

            tsmFilterSeparator.Visible = false;
            tsmAdvancedFilter.Visible = false;
            
            LoadFilterMenu(this.view);

        }

        private void tsmByMonth_Click(object sender, EventArgs e)
        {
            tspSeparator2.Visible = false;
            tsmByMonth.Checked = true;
            tsmByMonthYear.Checked = false;

            cboYear.Visible = false;
        }

        private void tsmByMonthYear_Click(object sender, EventArgs e)
        {
            tspSeparator2.Visible = true;
            tsmByMonth.Checked = false;
            tsmByMonthYear.Checked = true;

            cboYear.Visible = true;
        }


        private void cbChart_SelectedIndexChanged(object sender, EventArgs e)
        {
            DrawChart(cboChart.Text);
        }


        public void LoadTransactionByType(string type)
        {
            List<Model.Transaction> transactions = new List<Model.Transaction>();

            int month=cboMonth.SelectedIndex + 1;
            int year=int.Parse(cboYear.Text);

            if (type == TRANSACTION_ALL)
            {
                transactions = transactionRepository.GetAll(month,year);
            }
            else
            {
                switch(type)
                {
                    case TRANSACTION_INCOME :
                        transactions = transactionRepository.GetByType(TransactionType.Income, month, year);
                        break;
                    case TRANSACTION_EXPENSE :
                        transactions = transactionRepository.GetByType(TransactionType.Expense, month, year);
                        break;
                    case TRANSACTION_TRANSFER:
                        transactions = transactionRepository.GetByType(TransactionType.Transfer, month, year);
                        break;
                    case TRANSACTION_WITHDRAWL:
                        transactions = transactionRepository.GetByType(TransactionType.Withdrawl, month, year);
                        break;
                    case TRANSACTION_DEPOSIT :
                        transactions = transactionRepository.GetByType(TransactionType.Deposit, month, year);
                        break;
                 
                        
                }
            }

            lvwHistory.Items.Clear();

            foreach (Model.Transaction transaction in transactions)
            {
                RenderTransaction(transaction);
            }

            lblTotalIncome.Text = transactionRepository.GetTotalTransaction(CategoryType.Income, month, year).ToString("N0").Replace(",", ".");
            lblTotalExpense.Text = transactionRepository.GetTotalTransaction(CategoryType.Expense, month, year).ToString("N0").Replace(",", ".");
        }



        public void LoadAccountByType(string type)
        {
            List<Account> accounts = new List<Account>();

            if (type == ACCOUNT_ALL)
            {
                accounts = accountRepository.GetAll();
            }
            else
            {
                switch (type)
                {
                    case ACCOUNT_CASH :
                        accounts = accountRepository.GetByType(AccountType.Cash);
                        break;
                    case ACCOUNT_BANK :
                        accounts = accountRepository.GetByType(AccountType.Bank);
                        break;
                    case ACCOUNT_CREDIT_CARD :
                        accounts = accountRepository.GetByType(AccountType.CreditCard);
                        break;
                }
           }

            lvwAccount.Items.Clear();

            foreach (Account account in accounts)
            {
                RenderAccount(account);
            }

        }


        public void LoadCategoryByType(string type)
        {
            List<Category> categories = new List<Category>();

            if (type == CATAGORY_ALL)
            {
                categories = categoryRepository.GetAll();
            }
            else
            {
                switch (type)
                {
                    case CATAGORY_INCOME :
                        categories = categoryRepository.GetByType(CategoryType.Income);
                        break;
                    case CATAGORY_EXPENSE :
                        categories = categoryRepository.GetByType(CategoryType.Expense);
                        break;
                }
                
            }

            lvwCategory.Items.Clear();

            foreach (Category category in categories)
            {
                RenderCategory(category);
            }
        }


        public void LoadBudgetByType(string type)
        {
            int month = this.CurrentMonth;
            int year = this.CurrentYear;

            List<Budget> budgets = transactionRepository.GetAllBudget(month, year);
            this.Bar.Renderer = new BarRenderer(0, 100);

            olvBudget.SetObjects(budgets);
        }


        public void LoadReminderByStatus(string status)
        {
            List<Reminder> reminders = new List<Reminder>();

            int month = this.CurrentMonth;
            int year = this.CurrentYear;

            if (status == REMINDER_ALL)
            {
                reminders = reminderRepository.GetAll(month, year);
            }
            else
            {
                switch (status)
                {
                    case REMINDER_UNPAID:
                        reminders = reminderRepository.GetByStatus(ReminderType.Unpaid, month, year);
                        break;
                    case REMINDER_PAID:
                        reminders = reminderRepository.GetByStatus(ReminderType.Paid, month, year);
                        break;
                }
            }


            lvwReminder.Items.Clear();

            foreach (Reminder reminder in reminders)
            {
                RenderReminder(reminder);
            }
        }

        private void cboFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.isFirstLoad = false;

            switch (this.view)
            {
                case VIEW_TRANSACTION :
                    LoadTransactionByType(cboFilter.Text);
                    break;
                case VIEW_ACCOUNT:
                    LoadAccountByType(cboFilter.Text);
                    break;
                case VIEW_CATEGORY :
                    LoadCategoryByType(cboFilter.Text);
                    break;
                case VIEW_BUDGET :
                    LoadBudgetByType(cboFilter.Text);
                    break;
                case VIEW_REMINDER :
                    LoadReminderByStatus(cboFilter.Text);
                    break;

            }

            
        }

       

       

        private void tsmTransfer_Click(object sender, EventArgs e)
        {
            TransferUI frmTransfer = new TransferUI(this,lvwAccount.FocusedItem.SubItems[1].Text);
            frmTransfer.ShowDialog();
        }

        private void tsmWithdrawl_Click(object sender, EventArgs e)
        {
            WithdrawlUI frmWithdrawl = new WithdrawlUI(this,lvwAccount.FocusedItem.SubItems[1].Text);
            frmWithdrawl.ShowDialog();
        }

        private void tsmPayment_Click(object sender, EventArgs e)
        {
            PaymentUI frmPayment = new PaymentUI(this,lvwAccount.FocusedItem.SubItems[1].Text);
            frmPayment.ShowDialog();
        }


       

        private void lvwAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
                      
            EnableEditDelete();

            if (lvwAccount.FocusedItem.SubItems[2].Text == ACCOUNT_BANK)
            {
                lvwAccount.ContextMenuStrip = contextMenuStrip1;
                tspSeparator3.Visible = true;
                
                ctxTransfer.Visible = true;
                ctxWithdrawl.Visible = true;
                ctxDesposit.Visible = false;
                ctxPayment.Visible = false;

                tsbAction.Visible = true;
                tsmTransfer.Visible = true;
                tsmWithdrawl.Visible = true;
                tsmDeposit.Visible = false;
                tsmPayment.Visible = false;
       
            }
            else if (lvwAccount.FocusedItem.SubItems[2].Text == ACCOUNT_CREDIT_CARD)
            {
                lvwAccount.ContextMenuStrip = contextMenuStrip1;
                tspSeparator3.Visible = true;

                ctxTransfer.Visible = false;
                ctxWithdrawl.Visible = false;
                ctxDesposit.Visible = false;
                ctxPayment.Visible = true;

                tsbAction.Visible = true;
                tsmTransfer.Visible = false;
                tsmWithdrawl.Visible = false;
                tsmDeposit.Visible = false;
                tsmPayment.Visible = true;
            }
            else if (lvwAccount.FocusedItem.SubItems[2].Text == ACCOUNT_CASH)
            {
                lvwAccount.ContextMenuStrip = contextMenuStrip1;
                tspSeparator3.Visible = true;

                ctxTransfer.Visible = false;
                ctxWithdrawl.Visible = false;
                ctxDesposit.Visible = true;
                ctxPayment.Visible = false;

                tsbAction.Visible = true;
                tsmTransfer.Visible = false;
                tsmWithdrawl.Visible = false;
                tsmDeposit.Visible = true;
                tsmPayment.Visible = false;
       
            }
            else
            {
                lvwAccount.ContextMenuStrip = null;

                tspSeparator3.Visible = false;
                tsbAction.Visible = false;
            }

        }

        private void lvwAccount_DoubleClick(object sender, EventArgs e)
        {
            if (lvwAccount.Items.Count > 0)
            {
                AccountUI frmAccount = new AccountUI(this,new Guid(lvwAccount.FocusedItem.Text ));
                frmAccount.ShowDialog();

            }
        }

 
        private void lvwCategory_DoubleClick(object sender, EventArgs e)
        {
            if (lvwCategory.Items.Count > 0)
            {
                CategoryUI frmCategory = new CategoryUI(this, new Guid(lvwCategory.FocusedItem.Text));
                frmCategory.ShowDialog();
            }
        }

    

        private void lvwHistory_DoubleClick(object sender, EventArgs e)
        {
            if (lvwHistory.Items.Count > 0)
            {
                TransactionUI frmTransaction = new TransactionUI(new Guid(lvwHistory.FocusedItem.Text),this);
                frmTransaction.ShowDialog();
            }
        }

        private void lvwHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableEditDelete();
        }

        private void lvwCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableEditDelete();
        }
              

       
        private void lvwReminder_DoubleClick(object sender, EventArgs e)
        {
            if (lvwReminder.Items.Count != 0)
            {
                ReminderUI frmReminder = new ReminderUI(this,new Guid(lvwReminder.FocusedItem.Text));
                frmReminder.ShowDialog();
            }
        }

        private void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isFirstLoad)
            {
                tsmExportByYear.Text = "Tahun " + this.CurrentYear;

                LoadTransactionByType(cboFilter.Text);
                LoadBudgetByType(cboFilter.Text);
                LoadReminderByStatus(cboFilter.Text);
                DrawChart(cboChart.Text);
            }
        }

        private void lvwReminder_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = lvwReminder.Columns[e.ColumnIndex].Width;
        }

        private void olvBudget_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = olvBudget.Columns[e.ColumnIndex].Width;
        }

        private void lvwCategory_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = lvwCategory.Columns[e.ColumnIndex].Width;
        }

        private void lvwAccount_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = lvwAccount.Columns[e.ColumnIndex].Width;
        }

        private void lvwHistory_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = lvwHistory.Columns[e.ColumnIndex].Width;
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            switch (this.view)
            {
                case VIEW_TRANSACTION:
                
                    TransactionUI frmTransaction = new TransactionUI(new Guid(lvwHistory.FocusedItem.Text), this);
                    frmTransaction.ShowDialog();
                    break;
              
                case VIEW_ACCOUNT:

                    AccountUI frmAccount = new AccountUI(this,new Guid(lvwAccount.FocusedItem.Text));
                    frmAccount.ShowDialog();
                    break;

                case VIEW_CATEGORY:

                    CategoryUI frmCategory = new CategoryUI(this, new Guid(lvwCategory.FocusedItem.Text));
                    frmCategory.ShowDialog();
                    break;
                
                case VIEW_BUDGET:
                    LoadBudgetByType(cboFilter.Text);
                    break;

                case VIEW_REMINDER:

                    ReminderUI frmReminder = new ReminderUI(this, new Guid(lvwReminder.FocusedItem.Text));
                    frmReminder.ShowDialog();
                    break;
            }

        }


        private void lvwHistory_Leave(object sender, EventArgs e)
        {
            tsbEdit.Enabled = false;
            tsbDelete.Enabled = false;
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {

            switch (this.view)
            {
                case VIEW_TRANSACTION:

                    DeleteTransaction();
                    LoadTransactionByType(cboFilter.Text);
                    break;

                case VIEW_ACCOUNT:

                    if (accountRepository.IsAccountUsed(new Guid(lvwAccount.FocusedItem.Text)))
                    {
                        MessageBox.Show("Anda tidak dapat menghapus rekening ini. " + Environment.NewLine 
                            +lvwAccount.FocusedItem.SubItems[1].Text + " sudah digunakan di transaksi!",
                            "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        DeleteAccount();
                        LoadAccountByType(cboFilter.Text);
                    }
                   
                    break;

                case VIEW_CATEGORY:

                    string id = lvwCategory.FocusedItem.Text;
                    string name = lvwCategory.FocusedItem.SubItems[1].Text;
                    string group = lvwCategory.FocusedItem.SubItems[3].Text;

                    bool isUsed = false;

                    if (group == "Hutang" || group == "Piutang")
                    {
                        isUsed=categoryRepository.IsCategoryUsed(name);
                    }
                    else
                    {
                        isUsed=categoryRepository.IsCategoryUsed(new Guid(id));
                    }

                    if (isUsed)
                    {
                        MessageBox.Show("Anda tidak dapat menghapus kategori ini. " + Environment.NewLine
                            + lvwCategory.FocusedItem.SubItems[1].Text + " sudah digunakan di transaksi!",
                            "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        DeleteCategory();
                        LoadCategoryByType(cboFilter.Text);
                    }
                    
                    break;

                case VIEW_BUDGET:

                    LoadBudgetByType(cboFilter.Text);
                    break;
                
                case VIEW_REMINDER:

                    DeleteReminder();
                    LoadReminderByStatus(cboFilter.Text);

                    break;

            }

            DrawChart(cboChart.Text);
                           
        }

        
        private void DeleteTransaction()
        {
            if (MessageBox.Show("Anda yakin ingin menghapus transaksi ini?", "Konfirmasi",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {

                    Model.Transaction transaction = transactionRepository.GetById(new Guid(lvwHistory.FocusedItem.Text));
                  
               
                    transactionRepository.Delete(transaction);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal menghapus transaksi!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
      
            DisableEditDelete();
        }


        
        private void DeleteAccount()
        {
            string accountName = lvwAccount.FocusedItem.SubItems[1].Text;

            if (MessageBox.Show("Apakah Anda yakin untuk menghapus '" + accountName + "'?", "Konfirmasi",
                     MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    accountRepository.Delete(new Guid(lvwAccount.FocusedItem.Text));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal menghapus rekening!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
      
            DisableEditDelete();

        }


        private void DeleteCategory()
        {
            string categoryName = lvwCategory.FocusedItem.SubItems[1].Text;

            if (MessageBox.Show("Apakan Anda yakin ingin menghapus '" + categoryName + "'?", "Konfirmasi",
                     MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {

                    string name= lvwCategory.FocusedItem.SubItems[1].Text;
                    string group = lvwCategory.FocusedItem.SubItems[3].Text;

                    if (group == "Hutang" || group == "Piutang")
                    {
                        categoryRepository.Delete(name);
                    }
                    else
                    {
                        categoryRepository.Delete(new Guid(lvwCategory.FocusedItem.Text));
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal menghapus kategori!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            DisableEditDelete();

        }


        private void DeleteReminder()
        {
            string description = lvwReminder.FocusedItem.SubItems[1].Text;

            if (MessageBox.Show("Apakah Anda yakin ingin menghapus '" + description + "'?", "Konfirmasi",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    reminderRepository.Delete(new Guid(lvwReminder.FocusedItem.Text));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal menghapus reminder!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            DisableEditDelete();            
        }


          

        private void lvwAccount_Leave(object sender, EventArgs e)
        {
            DisableEditDelete();
        }

        private void lvwCategory_Leave(object sender, EventArgs e)
        {
            DisableEditDelete();
        }

        private void lvwReminder_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableEditDelete();
        }

        private void lvwReminder_Leave(object sender, EventArgs e)
        {
            DisableEditDelete();
        }

        private void ctxTransfer_Click(object sender, EventArgs e)
        {
            tsmTransfer_Click(sender, e);
        }

        private void ctxWithdrawl_Click(object sender, EventArgs e)
        {
            tsmWithdrawl_Click(sender,e);
        }

        private void ctxPayment_Click(object sender, EventArgs e)
        {
            tsmPayment_Click(sender, e);
        }



        private void ImportTransaction(string data)
        {           
            //format :
            //[date];[type];[category];[account];[amount]
            //11/08/2012;income;gaji istri;hsbc;3.000.000
                        
            try
            {                
                string[] datum = data.Split(';');

                string[] sDate = datum[0].Split('/');

                string day = sDate[0];
                string month = sDate[1];
                string year = sDate[2];


                DateTime date = DateTime.Parse(month + "/" + day + "/" + year);

                string type = datum[1].Substring(0, 1).ToUpper() + datum[1].Substring(1);
                string category = datum[2];
                string account = datum[3];
                string notes = datum[4];
                decimal amount = decimal.Parse(datum[5].Replace(".",string.Empty));
                

                Model.Transaction transaction = new Model.Transaction();

                transaction.Date = date;
                transaction.Type = type;
                transaction.Amount = amount;

               
                if (notes != "")
                {
                    transaction.Notes = notes.Substring(0, 1).ToUpper() + notes.Substring(1);
                }
                else
                {
                    transaction.Notes = string.Empty;
                }
                
                Category c = null;
                Account a = null;
                Account acc1 = null;
                Account acc2 = null;
                
                if (type.ToUpper() == IMPORT_INCOME || type.ToUpper() == IMPORT_EXPENSE)
                {
                    c = categoryRepository.GetByName(category);

                    if (c != null)
                    {
                        transaction.Category.Group = c.Group;
                        transaction.CategoryId = c.ID;
                    }

                    a = accountRepository.GetByName(account);
                    if (a != null)
                    {
                        transaction.AccountId = a.ID;
                    }
                }
                else
                {
                    acc1 = accountRepository.GetByName(category);
                    if (acc1 != null)
                    {
                        transaction.CategoryId = acc1.ID;
                    }

                    acc2 = accountRepository.GetByName(account);
                    if (acc2 != null)
                    {
                        transaction.AccountId = acc2.ID;
                    }
                }

                switch (type.ToUpper())
                {
                    case IMPORT_INCOME:
                        transaction.Description = "Pendapatan dari " + c.Name + " disimpan di " + a.Name;
                        break;
                    case IMPORT_EXPENSE:
                        transaction.Description = "Pengeluaran untuk " + c.Name + " dibayar dengan " + a.Name;
                        break;
                    case IMPORT_TRANSFER:
                        transaction.Description = "Transfer dari " + acc1.Name + " ke " + acc2.Name;
                        break;
                    case IMPORT_WITHDRAWL:
                        transaction.Description = "Penarikan tunai dari " + acc1.Name + " ke " + acc2.Name;
                        break;
                    case IMPORT_DEPOSIT:
                        transaction.Description = "Setor tunai dari " + acc1.Name + " ke " + acc2.Name;
                        break;
                    case IMPORT_PAYMENT:
                        transaction.Description = "Bayar " + acc1.Name + " dengan " + acc2.Name;
                        break;
                }

                if (type.ToUpper() == IMPORT_INCOME || type.ToUpper() == IMPORT_EXPENSE)
                {
                    if (c != null && a != null)
                    {
                        transactionRepository.Save(transaction);
                        this.successImport++;
                    }
                    else
                    {
                        errorData.Append(data + Environment.NewLine);
                        this.failedImport++;
                    }
                }
                else
                {
                    if (acc1 != null && acc2 != null)
                    {
                        transactionRepository.Save(transaction);
                        this.successImport++;
                    }
                    else
                    {
                        errorData.Append(data + Environment.NewLine);
                        this.failedImport++;
                    }
                }
            }
            catch (Exception ex)
            {
                errorData.Append(data + Environment.NewLine);
                this.failedImport++;
            }
            
        }



        private void OpenFileForImport(string fileName)
        {
            try
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    String line;


                    this.errorData = new StringBuilder();
                    this.successImport = 0;
                    this.failedImport = 0;

                    while ((line = sr.ReadLine()) != null)
                    {
                        ImportTransaction(line);
                    }


                    string s=errorData.ToString();

                    ImportUI frmImport = new ImportUI();

                    frmImport.SetMessage(this.successImport + " Data transaksi berhasil di-import, " + this.failedImport + " gagal!", errorData.ToString());

                   
                    frmImport.Show();


                    LoadTransactionByType(cboFilter.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal membaca file","Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

            

        private void tsmAdvancedFilter_Click(object sender, EventArgs e)
        {
            FilterUI frmFilter = new FilterUI(this);
            frmFilter.ShowDialog();
        }


        private void tsmImport_Click(object sender, EventArgs e)
        {
            string fileName = string.Empty;

            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter =
              "Text files (*.txt)|*.txt|CSV files (*.csv)|*.csv";

            dialog.InitialDirectory = "C:";
            dialog.Title = "Import Transaksi";

            if (dialog.ShowDialog() == DialogResult.OK)
                fileName = dialog.FileName;
            if (fileName == String.Empty)
                return;
            
            OpenFileForImport(fileName);
        }

        private void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            DrawChart(cboChart.Text);
        }



        private void tsmExport_Click(object sender, EventArgs e)
        {

            string fileName = string.Empty;

            SaveFileDialog dialog = new SaveFileDialog();

            dialog.Filter =
               "Text files (*.txt)|*.txt";

            dialog.InitialDirectory = "C:";
            dialog.Title = "Export Ringkasan Keuangan " + cboMonth.Text;

            if (dialog.ShowDialog() == DialogResult.OK)
                fileName = dialog.FileName;
            if (fileName == String.Empty)
                return;


            ExportFinanceSummary(fileName);
        }



        private void ExportFinanceSummary(string fileName)
        {
            try
            {
                using (StreamWriter sw = File.CreateText(fileName))
                {
                    sw.WriteLine("PERIODE " + cboMonth.Text.ToUpper() + " " + cboYear.Text);
                    sw.WriteLine("");

                    ExportCashAndBank(sw);
                    ExportCreditCard(sw);
                    ExportIncome(sw);
                    ExportExpense(sw);
                    ExportAccountPayable(sw);
                    ExportAccountReceivable(sw);

                    MessageBox.Show("Sukses export " + fileName, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal export file", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private decimal PopulateData(StreamWriter sw, Dictionary<String, decimal> data)
        {
            decimal totalAmount = 0;

            foreach (KeyValuePair<string, decimal> pair in data)
            {
                sw.WriteLine(pair.Key + " : " + pair.Value.ToString("N0").Replace(",", "."));
                totalAmount = totalAmount + pair.Value;
            }

            return totalAmount;
        }
        

        private void ExportCashAndBank(StreamWriter sw)
        {
            Dictionary<String, decimal> currentCashBank = chartRepository.GetCurrentCashAndBank();

            sw.WriteLine("SALDO KAS & BANK");
            sw.WriteLine("-------------------------------");

            decimal totalCurrentCashBank = PopulateData(sw, chartRepository.GetCurrentCashAndBank());
            
            sw.WriteLine("-------------------------------");
            sw.WriteLine("TOTAL : " + totalCurrentCashBank.ToString("N0").Replace(",", "."));
            sw.WriteLine();
            sw.WriteLine();

        }


        private void ExportCreditCard(StreamWriter sw)
        {
            sw.WriteLine("PEMAKAIAN KARTU KREDIT");
            sw.WriteLine("-------------------------------");

            decimal totalMonthlyCreditCard = PopulateData(sw, chartRepository.GetMonthlyCreditCard(this.CurrentMonth, this.CurrentYear));

            sw.WriteLine("-------------------------------");
            sw.WriteLine("TOTAL : " + totalMonthlyCreditCard.ToString("N0").Replace(",", "."));
            sw.WriteLine();
            sw.WriteLine();

        }



        private void ExportIncome(StreamWriter sw)
        {
            sw.WriteLine("PENDAPATAN");
            sw.WriteLine("-------------------------------");

            decimal totalMonthlyIncome = PopulateData(sw, chartRepository.GetMonthlyIncome(this.CurrentMonth, this.CurrentYear)); 

            sw.WriteLine("-------------------------------");
            sw.WriteLine("TOTAL : " + totalMonthlyIncome.ToString("N0").Replace(",", "."));
            sw.WriteLine();
            sw.WriteLine();

        }


        private void ExportExpense(StreamWriter sw)
        {
            sw.WriteLine("PENGELUARAN");
            sw.WriteLine("-------------------------------");

            decimal totalMonthlyExpense = PopulateData(sw, chartRepository.GetMonthlyExpense(this.CurrentMonth, this.CurrentYear));

            sw.WriteLine("-------------------------------");
            sw.WriteLine("TOTAL : " + totalMonthlyExpense.ToString("N0").Replace(",", "."));
            sw.WriteLine();
            sw.WriteLine();

        }


        private void ExportAccountPayable(StreamWriter sw)
        {
            sw.WriteLine("SALDO HUTANG");
            sw.WriteLine("-------------------------------");

            decimal totalAccountPayable = PopulateData(sw, chartRepository.GetAccountPayableBalance());

            sw.WriteLine("-------------------------------");
            sw.WriteLine("TOTAL : " + totalAccountPayable.ToString("N0").Replace(",", "."));
            sw.WriteLine();
            sw.WriteLine();

        }


        private void ExportAccountReceivable(StreamWriter sw)
        {
            sw.WriteLine("SALDO PIUTANG");
            sw.WriteLine("-------------------------------");

            decimal totalAccountReceivable = PopulateData(sw, chartRepository.GetAccountReceivableBalance());

            sw.WriteLine("-------------------------------");
            sw.WriteLine("TOTAL : " + totalAccountReceivable.ToString("N0").Replace(",", "."));
            sw.WriteLine();
            sw.WriteLine();

        }


        private void tsmDeposit_Click(object sender, EventArgs e)
        {
            DepositUI frmDeposit = new DepositUI(this,lvwAccount.FocusedItem.SubItems[1].Text);
            frmDeposit.Show();
        }

        private void ctxDesposit_Click(object sender, EventArgs e)
        {
            tsmDeposit_Click(sender, e);
        }

     

        private void tsmExportByMonth_Click(object sender, EventArgs e)
        {
             string fileName = string.Empty;
            
             SaveFileDialog dialog = new SaveFileDialog();

             dialog.Filter =
              "Text files (*.txt)|*.txt|CSV files (*.csv)|*.csv";

            dialog.InitialDirectory = "C:";
            dialog.Title = "Export Transaksi Bulan " + cboMonth.Text;

            if (dialog.ShowDialog() == DialogResult.OK)
                fileName = dialog.FileName;
            if (fileName == String.Empty)
                return;

            ExportTransaction(fileName, ExportType.ByMonth);


        }


        private void tsmExportByYear_Click(object sender, EventArgs e)
        {
            string fileName = string.Empty;

            SaveFileDialog dialog = new SaveFileDialog();

            dialog.Filter =
             "Text files (*.txt)|*.txt|CSV files (*.csv)|*.csv";

            dialog.InitialDirectory = "C:";
            dialog.Title = "Export Transaksi Tahun " + this.CurrentYear;

            if (dialog.ShowDialog() == DialogResult.OK)
                fileName = dialog.FileName;
            if (fileName == String.Empty)
                return;

            ExportTransaction(fileName, ExportType.ByYear);
        }




        private void ExportTransaction(string fileName,ExportType exportType)
        {
            try
            {
                using (StreamWriter sw = File.CreateText(fileName))
                {
                    List<Model.Transaction> transactions = transactionRepository.GetForExport(exportType, this.CurrentMonth, this.CurrentYear);

                    foreach (Model.Transaction t in transactions)
                    {

                        if (t.Type == "Income" || t.Type == "Expense")
                        {
                            Category category = categoryRepository.GetById(t.CategoryId);
                           
                            string categoryName = string.Empty;
                            if (category != null)
                            {
                                categoryName = category.Name;
                            }
                                                       
                            Account account = accountRepository.GetById(t.AccountId);

                            string accountName = string.Empty;
                            if (account != null)
                            {
                                accountName = account.Name;
                            }
                            
                            sw.WriteLine(t.Date.ToString("dd/MM/yyyy") + ";" + t.Type + ";" + categoryName + ";"
                                + accountName + ";" + t.Notes + ";" + t.Amount.ToString("N0").Replace(",", "."));
                        }
                        else
                        {
                            Account acc1 = accountRepository.GetById(t.CategoryId);

                            string accountFrom = string.Empty;
                            if (acc1 != null)
                            {
                                accountFrom = acc1.Name;
                            }

                            Account acc2 = accountRepository.GetById(t.AccountId);
                            
                            string accountTo = string.Empty;
                            if (acc2 != null)
                            {
                                accountTo = acc2.Name;
                            }

                            sw.WriteLine(t.Date.ToString("dd/MM/yyyy") + ";" + t.Type + ";" + accountFrom + ";"
                                + accountTo + ";" + t.Notes + ";" + t.Amount.ToString("N0").Replace(",", "."));
                        }

                    }

                    MessageBox.Show("Sukses export " + fileName, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal export file", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }



        private void tsmBackup_Click(object sender, EventArgs e)
        {

            string destinationFile = string.Empty;

            SaveFileDialog dialog = new SaveFileDialog();

            dialog.Filter =
             "MoneyCare database (*.sdf)|*.sdf";

            dialog.InitialDirectory = "C:";
            dialog.Title = "Backup Data";

            if (dialog.ShowDialog() == DialogResult.OK)
                destinationFile = dialog.FileName;
            if (destinationFile == String.Empty)
                return;

            string appPath = Path.GetDirectoryName(Application.ExecutablePath);

            try
            {
                if (!File.Exists(destinationFile))
                {
                    File.Copy(appPath + "\\moneycare.sdf", destinationFile);
                    MessageBox.Show("Backup data sukses", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("File " + destinationFile + " sudah ada!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal backup data!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void tsmRestore_Click(object sender, EventArgs e)
        {
            string fromFile = string.Empty;

            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter =
             "Money Care database (*.sdf)|*.sdf";

            dialog.InitialDirectory = "C:";
            dialog.Title = "Restore Data";

            if (dialog.ShowDialog() == DialogResult.OK)
                fromFile = dialog.FileName;
            if (fromFile == String.Empty)
                return;

            string appPath = Path.GetDirectoryName(Application.ExecutablePath);

            try
            {
                if (File.Exists(appPath + "\\moneycare.sdf"))
                {
                    if (File.Exists(appPath + "\\moneycare.old.sdf")) File.Delete(appPath + "\\moneycare.old.sdf");

                    File.Move(appPath + "\\moneycare.sdf", appPath + "\\moneycare.old.sdf");
                    File.Copy(fromFile, appPath + "\\moneycare.sdf");

                    MessageBox.Show("Restore data sukses, tutup aplikasi dan buka kembali untuk memuat data baru", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal restore data!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbSetting_Click(object sender, EventArgs e)
        {

            SettingUI frmSetting = new SettingUI();
            frmSetting.Show();
        }

      
      

       
       

    }
}
