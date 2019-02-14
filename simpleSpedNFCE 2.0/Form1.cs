using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace simpleSpedNFCE_2._0
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            txtNomeCont.Text = ConfigurationManager.AppSettings["NomeCont"];
            txtCPFCont.Text = ConfigurationManager.AppSettings["CPFCont"];
            txtCRCCont.Text = ConfigurationManager.AppSettings["CRCCont"];
            txtCNPJCont.Text = ConfigurationManager.AppSettings["CNPJESC"];
            txtEmailCont.Text = ConfigurationManager.AppSettings["emailCont"];
            txtCEPCont.Text = ConfigurationManager.AppSettings["CEPCont"];
            txtLogradouroCont.Text = ConfigurationManager.AppSettings["logradouroCont"];
            txtNumeroCont.Text = ConfigurationManager.AppSettings["numCont"];
            txtComplementoCont.Text = ConfigurationManager.AppSettings["compCont"];
            txtBairroCont.Text = ConfigurationManager.AppSettings["bairroCont"];
            txtFoneCont.Text = ConfigurationManager.AppSettings["foneCont"];
            txtFaxCont.Text = ConfigurationManager.AppSettings["faxCont"];
            txtCodMunCont.Text = ConfigurationManager.AppSettings["CodMunCont"];
            txtRazaoSocialEmp.Text = ConfigurationManager.AppSettings["razaoSocialEmp"];
            txtCPFEmp.Text = ConfigurationManager.AppSettings["CNPJEmp"];
            cbUFEmp.Text = ConfigurationManager.AppSettings["UFEmp"];
            txtIEEmp.Text = ConfigurationManager.AppSettings["inscricaoEstadual"];
            txtCodMunEmp.Text = ConfigurationManager.AppSettings["codMunEmp"];
            txtNomeFanEmp.Text = ConfigurationManager.AppSettings["fantasiaEmp"];
            txtCEPEmp.Text = ConfigurationManager.AppSettings["cepEmp"];
            txtLogEmp.Text = ConfigurationManager.AppSettings["logEmp"];
            txtNumEmp.Text = ConfigurationManager.AppSettings["numEmp"];
            txtCompEmp.Text = ConfigurationManager.AppSettings["compEmp"];
            txtBairroEmp.Text = ConfigurationManager.AppSettings["bairroEmp"];
            txtFoneEmp.Text = ConfigurationManager.AppSettings["foneEmp"];
            txtFaxEmp.Text = ConfigurationManager.AppSettings["faxEmp"];
            txtEmailEmp.Text = ConfigurationManager.AppSettings["emailEmp"];
            txtDtIni.Text = ConfigurationManager.AppSettings["dtIni"];
            txtDtFin.Text = ConfigurationManager.AppSettings["dtFin"];
            cbCRT.Text = ConfigurationManager.AppSettings["CRT"];
            cbPerfil.Text = ConfigurationManager.AppSettings["Perfil"];
        }

        private void label1_Click(object sender, EventArgs e)
        {
           
        }

        private void btProcurarPastaXML_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fdb = new FolderBrowserDialog();
            fdb.ShowDialog();
            txtCaminhoXML.Text = fdb.SelectedPath;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fdb = new FolderBrowserDialog();
            fdb.ShowDialog();
            txtArquivoSPED.Text = fdb.SelectedPath;
        }

        private void btGravarEsc_Click(object sender, EventArgs e)
        {
            Configuration config =
            ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["razaoSocialEmp"].Value = txtRazaoSocialEmp.Text;
            config.AppSettings.Settings["CNPJEmp"].Value = txtCPFEmp.Text;
            config.AppSettings.Settings["UFEmp"].Value = cbUFEmp.Text;
            config.AppSettings.Settings["inscricaoEstadual"].Value = txtIEEmp.Text;
            config.AppSettings.Settings["codMunEmp"].Value = txtCodMunEmp.Text;
            config.AppSettings.Settings["dtIni"].Value = txtDtIni.Text;
            config.AppSettings.Settings["dtFin"].Value = txtDtFin.Text;
            config.AppSettings.Settings["fantasiaEmp"].Value = txtNomeFanEmp.Text;
            config.AppSettings.Settings["cepEmp"].Value = txtCEPEmp.Text;
            config.AppSettings.Settings["logEmp"].Value = txtLogEmp.Text;
            config.AppSettings.Settings["numEmp"].Value = txtNumEmp.Text;
            config.AppSettings.Settings["compEmp"].Value = txtCompEmp.Text;
            config.AppSettings.Settings["bairroEmp"].Value = txtBairroEmp.Text;
            config.AppSettings.Settings["foneEmp"].Value = txtFoneEmp.Text;
            config.AppSettings.Settings["faxEmp"].Value = txtFaxEmp.Text;
            config.AppSettings.Settings["emailEmp"].Value = txtEmailEmp.Text;
            config.AppSettings.Settings["CRT"].Value = cbCRT.Text;
            config.AppSettings.Settings["Perfil"].Value = cbPerfil.Text;
            config.Save(ConfigurationSaveMode.Modified);
        }

        private void btGravarCont_Click(object sender, EventArgs e)
        {
            Configuration config =
            ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["nomeCont"].Value = txtNomeCont.Text;
            config.AppSettings.Settings["CPFCont"].Value = txtCPFCont.Text;
            config.AppSettings.Settings["CRCCont"].Value = txtCRCCont.Text;
            config.AppSettings.Settings["CNPJEsc"].Value = txtCNPJCont.Text;
            config.AppSettings.Settings["emailCont"].Value = txtEmailCont.Text;
            config.AppSettings.Settings["CEPCont"].Value = txtCEPCont.Text;
            config.AppSettings.Settings["logradouroCont"].Value = txtLogradouroCont.Text;
            config.AppSettings.Settings["numCont"].Value = txtNumeroCont.Text;
            config.AppSettings.Settings["compCont"].Value = txtComplementoCont.Text;
            config.AppSettings.Settings["bairroCont"].Value = txtBairroCont.Text;
            config.AppSettings.Settings["foneCont"].Value = txtFoneCont.Text;
            config.AppSettings.Settings["faxCont"].Value = txtFaxCont.Text;
            config.AppSettings.Settings["codMunCont"].Value = txtCodMunCont.Text;
            config.Save(ConfigurationSaveMode.Modified);
        }
        
        private void btValidar_Click(object sender, EventArgs e)
        {
            Escrituracao esc = new Escrituracao(txtCaminhoXML.Text, txtArquivoSPED.Text);
            lbStatus.Text = "Montando dados da Escrituração";
            esc.razãoSocial = txtRazaoSocialEmp.Text;
            esc.cnpj = txtCPFEmp.Text;
            esc.uf = cbUFEmp.Text;
            esc.ie = txtIEEmp.Text;
            esc.codMunEmp = txtCodMunEmp.Text;
            esc.nomeFantasia = txtNomeFanEmp.Text;
            esc.cep = txtCEPEmp.Text;
            esc.logradouro = txtLogEmp.Text;
            esc.numero = txtNumEmp.Text;
            esc.complemento = txtCompEmp.Text;
            esc.bairro = txtBairroEmp.Text;
            esc.fone = txtFoneEmp.Text;
            esc.fax = txtFaxEmp.Text;
            esc.email = txtEmailEmp.Text;
            esc.numero = txtNumEmp.Text;
            esc.setCRT(cbCRT.Text);
            esc.dtIni = txtDtIni.Text.Replace("/", "");
            esc.dtFin = txtDtFin.Text.Replace("/", "");
            esc.setContador(popularContador());
            esc.setNotas();
            esc.setParticipantes();
            esc.setUnidades();
            esc.setProdutos();
            esc.setCFOP();
            esc.montagemBloco0000();
            esc.montagemRegistro0001();
            esc.montagemRegistro0005();
            esc.montagemBloco0100();            
            esc.montagemBloco0150();
            esc.montagemBloco0190();
            esc.montagemBloco0200();
            esc.montagemBloco0400();
            esc.montagemBlocoVazio("B");
            esc.montagemBlocoC();
            esc.montagemBlocoVazio("D");
            esc.montagemBlocoVazio("E");
            esc.montagemBlocoVazio("G");
            esc.montagemBlocoVazio("H");
            esc.montagemBlocoVazio("K");
            esc.montagemBlocoVazio("1");
            esc.montagemBloco9();
            lbStatus.Text = "Arquivo gerado com sucesso!";
        }
        private Contador popularContador()
        {
            Contador contador = new Contador();
            contador.nome = txtNomeCont.Text;
            contador.cpf = txtCPFCont.Text;
            contador.crc = txtCRCCont.Text;
            contador.cnpjEsc = txtCNPJCont.Text;
            contador.email = txtEmailCont.Text;
            contador.cep = txtCEPCont.Text;
            contador.logradouro = txtLogradouroCont.Text;
            contador.numero = txtNumeroCont.Text;
            contador.complemento = txtComplementoCont.Text;
            contador.bairro = txtBairroCont.Text;
            contador.fone = txtFoneCont.Text;
            contador.fax = txtFaxCont.Text;
            contador.codMunicipio = txtCodMunCont.Text;

            return contador;
        }

    }
}
