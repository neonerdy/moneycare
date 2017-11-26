using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using FinMan.Model;
using EntityMap;

namespace FinanceManager
{
    public partial class Dashboard : Form
    {

        private IRegistry registry;
        private AccountRepository accountRepository;
        private CashFlowRepository cashFlowRepository;
        private ChartRepository chartRepository;
        private SettingRepository settingRepository;

        public Dashboard()
        {
            registry = new RepositoryRegistry();
            registry.Configure();
            
            accountRepository = RepositoryFactory.GetObject<AccountRepository>(RepositoryId.ACCOUNT_REPOSITORY);
            cashFlowRepository = RepositoryFactory.GetObject<CashFlowRepository>(RepositoryId.CASHFLOW_REPOSITORY);
            chartRepository = RepositoryFactory.GetObject<ChartRepository>(RepositoryId.CHART_REPOSITORY);
            settingRepository = RepositoryFactory.GetObject<SettingRepository>(RepositoryId.SETTING_REPOSITORY);


            InitializeComponent();
        }


        private void LoadAccount()
        {
            List<Account> accounts = accountRepository.GetAll();

            cbAccount.Items.Clear();
            foreach (Account account in accounts)
            {
                cbAccount.Items.Add(account.Name);
            }

        }

        private void LoadCashFlowByCategory(string category)
        {
            List<CashFlow> cashFlows = new List<CashFlow>();

            switch (category)
            {
                case "Cash & Bank" :
                    cashFlows=cashFlowRepository.GetByCashAndBank(cbMonth.SelectedIndex + 1, DateTime.Now.Year);
                    break;
                case "Income" :
                    cashFlows=cashFlowRepository.GetByIncome(cbMonth.SelectedIndex + 1, DateTime.Now.Year);
                    break;
                case "Expense" :
                    cashFlows=cashFlowRepository.GetByExpense(cbMonth.SelectedIndex + 1, DateTime.Now.Year);
                    break;

                case "Account Payable" :
                    cashFlows = cashFlowRepository.GetByAccountPayable(cbMonth.SelectedIndex + 1, DateTime.Now.Year);
                    break;

                case "Account Receivable":
                    cashFlows = cashFlowRepository.GetByAccountReceivable(cbMonth.SelectedIndex + 1, DateTime.Now.Year);
                    break;

                case "Asset":
                    cashFlows = cashFlowRepository.GetByAsset(cbMonth.SelectedIndex + 1, DateTime.Now.Year);
                    break;
            }


            lvwCashFlow.Items.Clear();

            foreach (CashFlow cashFlow in cashFlows)
            {
                ListViewItem item = new ListViewItem(cashFlow.ID.ToString());

                item.SubItems.Add(cashFlow.Date.ToShortDateString());
                item.SubItems.Add(cashFlow.Description);
                item.SubItems.Add(cashFlow.Amount.ToString("N0"));

                lvwCashFlow.Items.Add(item);

            }
        }

        private void LoadCashFlow()
        {

            List<CashFlow> cashFlows = cashFlowRepository.GetAll(cbMonth.SelectedIndex+1,DateTime.Now.Year);

            lvwCashFlow.Items.Clear();

            foreach (CashFlow cashFlow in cashFlows)
            {
                ListViewItem item = new ListViewItem(cashFlow.ID.ToString());

                item.SubItems.Add(cashFlow.Date.ToShortDateString());
                item.SubItems.Add(cashFlow.Description);
                item.SubItems.Add(cashFlow.Amount.ToString("N0"));

                lvwCashFlow.Items.Add(item);

            }

       
            decimal totalIncome= cashFlowRepository.GetTotalCashFlow(CashFlowType.Income,cbMonth.SelectedIndex + 1, DateTime.Now.Year);
            decimal totalExpense=cashFlowRepository.GetTotalCashFlow(CashFlowType.Expense, cbMonth.SelectedIndex + 1, DateTime.Now.Year);
            decimal netIncome=totalIncome-totalExpense;

            lblTotalIncome.Text = totalIncome.ToString("N0");
            lblTotalExpense.Text = totalExpense.ToString("N0");
            lblNetIncome.Text = netIncome.ToString("N0");
        
        }


        private void Dashboard_Load(object sender, EventArgs e)
        {
            
            LoadMonth();
            LoadAccount();
            LoadHistoryMenu();
            LoadChartMenu();
            LoadCashFlow();
            
            
        }

        private void LoadHistoryMenu()
        {
            cbHistory.Items.Add("All");

            cbHistory.Items.Add("Cash & Bank");
            cbHistory.Items.Add("Income");
            cbHistory.Items.Add("Expense");
            cbHistory.Items.Add("Account Payable");
            cbHistory.Items.Add("Account Receivable");
            cbHistory.Items.Add("Asset");

            cbHistory.Text = "All";
        }

        private void LoadChartMenu()
        {
            cbChart.Items.Add("Cash & Bank");
            cbChart.Items.Add("Income");
            cbChart.Items.Add("Expense");
            cbChart.Items.Add("Account Payable");
            cbChart.Items.Add("Account Receivable");
            cbChart.Items.Add("Asset");
            cbChart.Items.Add("Financial Checkup");
        
            cbChart.Text = "Cash & Bank";
        }

        private void LoadMonth()
        {
            cbMonth.Items.Add("January");
            cbMonth.Items.Add("February");
            cbMonth.Items.Add("March");
            cbMonth.Items.Add("April");
            cbMonth.Items.Add("May");
            cbMonth.Items.Add("June");
            cbMonth.Items.Add("July");
            cbMonth.Items.Add("August");
            cbMonth.Items.Add("September");
            cbMonth.Items.Add("October");
            cbMonth.Items.Add("November");
            cbMonth.Items.Add("Desember");

            switch (DateTime.Now.Month)
            {
                case 1: cbMonth.Text = "January";
                    break;
                case 2: cbMonth.Text = "February";
                    break;
                case 3: cbMonth.Text = "March";
                    break;
                case 4: cbMonth.Text = "April";
                    break;
                case 5: cbMonth.Text = "May";
                    break;
                case 6: cbMonth.Text = "June";
                    break;
                case 7: cbMonth.Text = "July";
                    break;
                case 8: cbMonth.Text = "August";
                    break;
                case 9: cbMonth.Text = "September";
                    break;
                case 10: cbMonth.Text = "October";
                    break;
                case 11: cbMonth.Text = "November";
                    break;
                case 12: cbMonth.Text = "Desember";
                    break;
            }
        }


        private void DrawFinanceRatio()
        {

            //Liquidity Ratio

            int currentMonth = cbMonth.SelectedIndex + 1;
            int currentYear=DateTime.Now.Year;

            decimal emergencyFund = chartRepository.GetEmergencyFund(currentMonth,currentYear);
            decimal expense = chartRepository.GetExpense(currentMonth, currentYear);
            decimal debt = chartRepository.GetDebt(currentMonth, currentYear);
            decimal asset = chartRepository.GetAsset(currentMonth, currentYear);
            decimal income = chartRepository.GetIncome(currentMonth, currentYear);
            decimal investment = chartRepository.GetInvestment(currentMonth, currentYear);
            decimal saving = chartRepository.GetSaving(currentMonth, currentYear);
          
            decimal expenseAndDebt=expense+debt;
            lblFundBalance.Text = "Balance ( " + settingRepository.GetValue("EmergencyFund") + " )";
          
            if (expenseAndDebt != 0)
            {
               
                decimal liquidityRatio = emergencyFund / expenseAndDebt;
                lblLiquidityRatio.Text = liquidityRatio.ToString("N1");
                lblFundCurrent.Text = emergencyFund.ToString("N0");
                lblFundMustHave.Text = (expenseAndDebt * 6).ToString("N0");

                decimal percent = (emergencyFund / (expenseAndDebt * 6)) * 100;

                lblFundPercent.Text = percent.ToString("N1") + " %";
                decimal pnlLiquidWidth=300*(percent/100);

                pnlLiquidityRatio.Width = (int)pnlLiquidWidth;

                if (liquidityRatio < 6)
                {
                    lblLiquidityStatus.Text = "Not Good";
                    lblLiquidityStatus.ForeColor = Color.DarkRed;
                }
                else
                {
                    lblLiquidityStatus.Text = "Good";
                    lblLiquidityStatus.ForeColor = Color.DarkGreen;
                }
            }
            else
            {
                lblFundCurrent.Text = "0";
                lblFundMustHave.Text = "0";
                lblFundPercent.Text = "0 %";
                lblLiquidityStatus.Text = "Not Good";
                lblLiquidityStatus.ForeColor = Color.DarkRed;
                pnlLiquidityRatio.Width = 0;

            }


            //Liquid Asset to Net Worth Ratio

            decimal netWorth = asset - debt;

            if (netWorth != 0)
            {
                decimal liquidToNetWorthRatio = emergencyFund / netWorth;
                lblLiquidToNWRatio.Text = liquidToNetWorthRatio.ToString("N1") + " %";
               
                if (liquidToNetWorthRatio >= 15)
                {
                    lblLiquidToNWStatus.Text = "Good";
                    lblLiquidToNWStatus.ForeColor = Color.DarkGreen;
                }
                else
                {
                    lblLiquidToNWStatus.Text = "Not Good";
                    lblLiquidToNWStatus.ForeColor = Color.DarkRed;
                }


                decimal pnlLiquidToNWWidth = 300 * (liquidToNetWorthRatio / 100);

                pnlLiquidToNWRatio.Width = (int)pnlLiquidToNWWidth;
            
            }


            //Solvency Ratio

            if (asset != 0)
            {
                decimal solvencyRatio = netWorth / asset;
                lblSolvencyRatio.Text = solvencyRatio.ToString("N1") + " %";

                if (solvencyRatio > 35)
                {
                    lblSolvencyStatus.Text = "Good";
                    lblSolvencyStatus.ForeColor = Color.DarkGreen;
                }
                else
                {
                    lblSolvencyStatus.Text = "Not Good";
                    lblSolvencyStatus.ForeColor = Color.DarkRed;
                }

                decimal pnlSolvencyWidth=300*(solvencyRatio/100);
                pnlSolvencyRatio.Width = (int)pnlSolvencyWidth;
            
            }


            //Debt to Asset Ratio

            if (asset != 0)
            {
                decimal debtToAssetRatio = debt / asset;
                lblDebtToAssetRatio.Text = debtToAssetRatio.ToString("N1") + " %";
                if (debtToAssetRatio < 50)
                {
                    lblDebtToAssetStatus.Text = "Good";
                    lblDebtToAssetStatus.ForeColor = Color.DarkGreen;
                }
                else
                {
                    lblDebtToAssetStatus.Text = "Not Good";
                    lblDebtToAssetStatus.ForeColor = Color.DarkRed;
                }

                decimal pnlDebtToAsset = 300 * (debtToAssetRatio / 100);
                pnlDebtToAssetRatio.Width = (int)pnlDebtToAsset;
            }
            else
            {

            }



            //Debt Service Ratio

            if (income != 0)
            {

                decimal debtServiceRatio = debt / income;
                lblDebtServiceRatio.Text = debtServiceRatio.ToString("N1") + " %";

                if (debtServiceRatio < 35)
                {
                    lblDebtServiceStatus.Text = "Good";
                    lblDebtServiceStatus.ForeColor = Color.DarkGreen;
                }
                else if (debtServiceRatio >= 45)
                {
                    lblDebtServiceStatus.Text = "Danger";
                    lblDebtServiceStatus.ForeColor = Color.DarkRed;
                }
                else
                {
                    lblDebtServiceStatus.Text = "Not Good";
                    lblDebtServiceStatus.ForeColor = Color.DarkRed;
                }

                decimal pnlDebtServiceWidth=300*(debtServiceRatio/100);
                pnlDebtServiceRatio.Width = (int)pnlDebtServiceWidth;
            }


            //Net Investment

            if (netWorth != 0)
            {
                decimal netInvestmentRatio = investment / netWorth;
                lblInvestmentRatio.Text = netInvestmentRatio.ToString("N1") + " %";
                if (netInvestmentRatio >= 50)
                {
                    lblInvestmentStatus.Text = "Good";
                    lblInvestmentStatus.ForeColor = Color.DarkGreen;
                }
                else
                {
                    lblInvestmentStatus.Text = "Not Good";
                    lblInvestmentStatus.ForeColor = Color.DarkRed;
                }

                decimal pnlInvestmentWidth=300*(netInvestmentRatio/100);
                pnlInvestmentRatio.Width = (int)pnlInvestmentWidth;


            }
            //Saving Ratio

          
            if (income != 0)
            {
                decimal savingRatio = saving / income;
                lblSavingRatio.Text = savingRatio.ToString("N1") + " %";
                if (savingRatio < 10)
                {
                    lblSavingStatus.Text = "Not Good";
                    lblSavingStatus.ForeColor = Color.DarkRed;
                }
                else
                {
                    lblSavingStatus.Text = "Good";
                    lblSavingStatus.ForeColor = Color.DarkGreen;
                }

                decimal pnlSavingWidth = 300 * (savingRatio / 100);
                pnlSavingRatio.Width = (int)pnlSavingWidth;

            }
            else
            {
                pnlSavingRatio.Width = 0;
                lblSavingStatus.Text = string.Empty;
            }


            pnlFinancialCheckup.Visible = true;
        }


        private void DrawChart()
        {

            List<string> name = new List<string>();
            List<decimal> amount = new List<decimal>();

            Dictionary<string, decimal> monthlyCashBank = new Dictionary<string, decimal>();
            Dictionary<string, decimal> monthlyIncome = new Dictionary<string, decimal>();
            Dictionary<string, decimal> monthlyExpense = new Dictionary<string,decimal>();
            Dictionary<string, decimal> monthlyDebt = new Dictionary<string, decimal>();
            Dictionary<string, decimal> monthlyAR = new Dictionary<string, decimal>();
            Dictionary<string, decimal> currentAsset = new Dictionary<string, decimal>();

            int currentMonth = cbMonth.SelectedIndex + 1;
            int currentYear = DateTime.Now.Year;

            switch (cbChart.Text)
            {
                case "Cash & Bank" :

                    pnlFinancialCheckup.Visible = false;
                    monthlyCashBank = chartRepository.GetMonthlyCashAndBank(currentMonth,currentYear);

                    foreach (KeyValuePair<string, decimal> pair in monthlyCashBank)
                    {
                        name.Add(pair.Key);
                        amount.Add(pair.Value);
                    }

                    break;

                case "Income" :
                
                     pnlFinancialCheckup.Visible = false;

                     monthlyIncome=chartRepository.GetMonthlyIncome(currentMonth,currentYear);
                    foreach (KeyValuePair<string, decimal> pair in monthlyIncome)
                    {
                        name.Add(pair.Key);
                        amount.Add(pair.Value);
                    }

                    break;
                
                case "Expense" :

                     pnlFinancialCheckup.Visible = false;

                     monthlyExpense = chartRepository.GetMonthlyExpense(currentMonth,currentYear);

                    foreach (KeyValuePair<string, decimal> pair in monthlyExpense)
                    {
                        name.Add(pair.Key);
                        amount.Add(pair.Value);
                    }

                    break;

                case "Account Payable" :

                      pnlFinancialCheckup.Visible = false;

                      monthlyDebt = chartRepository.GetMonthlyDebt(currentMonth,currentYear);

                     foreach (KeyValuePair<string, decimal> pair in monthlyDebt)
                     {
                         name.Add(pair.Key);
                         amount.Add(pair.Value);
                     }

                     break;

                case "Account Receivable":

                     pnlFinancialCheckup.Visible = false;

                     monthlyAR = chartRepository.GetMonthlyAccountReceivable(currentMonth,currentYear);

                    foreach (KeyValuePair<string, decimal> pair in monthlyAR)
                    {
                        name.Add(pair.Key);
                        amount.Add(pair.Value);
                    }

                    break;
                
                case "Asset":

                     pnlFinancialCheckup.Visible = false;

                     currentAsset = chartRepository.GetCurrentAsset(currentMonth,currentYear);

                    foreach (KeyValuePair<string, decimal> pair in currentAsset)
                    {
                        name.Add(pair.Key);
                        amount.Add(pair.Value);
                    }

                  break;

                case "Financial Checkup" :
                  pnlFinancialCheckup.Visible = true;
                  break;
            }


          
            chart1.Series["Series1"].Points.DataBindXY(name, amount);
            chart1.Series["Series1"].Label = "#PERCENT";
          
            chart1.Series["Series1"].LegendText = "#AXISLABEL : #VALY{N0}";


            chart1.Series["Series1"].ChartType = SeriesChartType.Doughnut;




            //else if (cbChart.Text == "Income Per Year")
            //{
            //    chart1.Series["Series1"].Points.DataBindXY(xValues, yValues);
            //    chart1.Series["Series1"].ChartType = SeriesChartType.Column;
            //}
            
         
            // Set labels style
            chart1.Series["Series1"]["PieLabelStyle"] = "Outside";

            // Set Doughnut radius percentage
            chart1.Series["Series1"]["DoughnutRadius"] = "60";

            // Explode data point with label "Italy"
           // chart1.Series["Series1"].Points[3]["Exploded"] = "true";

            // Enable 3D
            //chart1.ChartAreas[0].Area3DStyle.Enable3D = true;

            // Set drawing style
            //Default, SoftEdge, Concave

            chart1.Series["Series1"]["PieDrawingStyle"] = "Concave";


            //chart1.Legends["Legend1"].HeaderSeparator = LegendSeparatorStyle.Line;
            //chart1.Legends["Legend1"].HeaderSeparatorColor = Color.Gray;

            //LegendCellColumn firstColumn = new LegendCellColumn();
            //firstColumn.ColumnType = LegendCellColumnType.SeriesSymbol;
            //firstColumn.HeaderText = "Color";
            //firstColumn.HeaderBackColor = Color.WhiteSmoke;
            //chart1.Legends["Legend1"].CellColumns.Add(firstColumn);


            //LegendCellColumn secondColumn = new LegendCellColumn();
            //secondColumn.ColumnType = LegendCellColumnType.Text;
            //secondColumn.HeaderText = "Account";
            //secondColumn.Text = "#N1";
            //secondColumn.HeaderBackColor = Color.WhiteSmoke;
            //chart1.Legends["Legend1"].CellColumns.Add(secondColumn);


            chart1.Visible = true;
         
            
        }

        private void btnAccount_Click(object sender, EventArgs e)
        {
            frmAccount account = new frmAccount();
            account.Show();
        }

        private void btnCashFlow_Click(object sender, EventArgs e)
        {
            frmCashFlow cashFlow = new frmCashFlow();
            cashFlow.Show();
        }

        private void cbChart_SelectedIndexChanged(object sender, EventArgs e)
        {
            DrawGraph();
        }

        private void DrawGraph()
        {
            if (cbChart.Text == "Financial Checkup")
            {
                if (lvwCashFlow.Items.Count != 0)
                {
                    DrawFinanceRatio();
                }
                else
                {
                    pnlFinancialCheckup.Visible = false;
                    chart1.Visible = false;
                }
            }
            else
            {
               DrawChart();
             
            }
        }

      

        private void cbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCashFlow();
            DrawGraph();
        }

        private void lvwCashFlow_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void lvwCashFlow_DoubleClick(object sender, EventArgs e)
        {
            frmCashFlow cashFlow = new frmCashFlow(lvwCashFlow.FocusedItem.Text);
            cashFlow.ShowDialog();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadCashFlow();
            DrawChart();
            cbHistory.Text = "All";
        }

        private void cashFlowToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void btnChart_Click(object sender, EventArgs e)
        {
            frmChart chart = new frmChart();
            chart.Show();
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            frmSetting setting = new frmSetting();
            setting.Show();
        }

        private void cbHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbHistory.Text == "All")
            {
                LoadCashFlow();
            }
            else
            {
                LoadCashFlowByCategory(cbHistory.Text);
            }

           
        }

        private void lvwCashFlow_ColumnClick(object sender, ColumnClickEventArgs e)
        {
           
        }

        private void cbMonth_Click(object sender, EventArgs e)
        {

        }

    }
}
