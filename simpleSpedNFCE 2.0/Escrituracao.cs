using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using System.Xml;
using System.Globalization;

namespace simpleSpedNFCE_2._0
{
    class Escrituracao
    {
        private string caminhoXml;
        private string caminhoArquivo;
        private Contador cont;
        private ArrayList participantes = new ArrayList();
        private ArrayList unidades = new ArrayList();
        private ArrayList produtos = new ArrayList();
        private ArrayList notas = new ArrayList();
        private ArrayList registros = new ArrayList();
        private ArrayList regAnaliticos = new ArrayList();
        private ArrayList CFOP = new ArrayList();
        public string razãoSocial;
        public string cnpj;
        public string uf;
        public string ie;
        public string codMunEmp;
        public string nomeFantasia;
        public string cep;
        public string logradouro;
        public string numero;
        public string complemento;
        public string bairro;
        public string fone;
        public string fax;
        public string email;
        public string CRT;
        public string Perfil;
        public string dtIni;
        public string dtFin;
        public int qtdLinhas = 0;

        public Escrituracao(string caminhoXml, string caminhoArquivo)
        {
            this.caminhoXml = caminhoXml;
            this.caminhoArquivo = caminhoArquivo;
        }

        public void setContador(Contador cont)
        {
            this.cont = cont;
        }
        public void setCRT(string crt)
        {
            if (crt.Equals("1 - Regime Normal"))
            {
                CRT = "1";
            }
            else if (crt.Equals("2 - Simples Nacional"))
            {
                CRT = "2";
            }
        }
        public void montagemBloco0000()
        {
            FileStream fs = new FileStream(@caminhoArquivo+@"\sped"+dtIni+"a"+dtFin+".txt",FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine("|0000|013|0|" + dtIni.Replace("/", "") + "|" + dtFin.Replace("/", "")+"|"+campoFormatado(razãoSocial)+
                campoFormatado(cnpj)+"|"+campoFormatado(uf)+campoFormatado(ie)+campoFormatado(codMunEmp)+"||B|1|");
            sw.Close();
            fs.Close();
            registros.Add(setRegistro("0000"));

        }
        public void montagemRegistro0001()
        {
            FileStream fs = new FileStream(@caminhoArquivo + @"\sped" + dtIni + "a" + dtFin + ".txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine("|0001|0|");
            registros.Add(setRegistro("0001"));
            sw.Close();
            fs.Close();
        }
        public void montagemRegistro0005()
        {
            FileStream fs = new FileStream(@caminhoArquivo + @"\sped" + dtIni + "a" + dtFin + ".txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine("|0005|"+campoFormatado(nomeFantasia)+campoFormatado(cep)+campoFormatado(logradouro)+
                campoFormatado(numero)+campoFormatado(complemento)+campoFormatado(bairro)+campoFormatado(fone)+campoFormatado(fax)+campoFormatado(email));
            registros.Add(setRegistro("0005"));
            sw.Close();
            fs.Close();
        }
        public void montagemBloco0100()
        {
            FileStream fs = new FileStream(@caminhoArquivo+@"\sped"+dtIni+"a"+dtFin+".txt",FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine("|0100|" + campoFormatado(cont.nome)+campoFormatado(cont.cpf)+campoFormatado(cont.crc)
                +campoFormatado(cont.cnpjEsc)+campoFormatado(cont.cep)+campoFormatado(cont.logradouro)+campoFormatado(cont.numero)
                +campoFormatado(cont.complemento)+campoFormatado(cont.bairro)+campoFormatado(cont.fone)+campoFormatado(cont.fax)
                +campoFormatado(cont.email)+campoFormatado(cont.codMunicipio));
            registros.Add(setRegistro("0100"));
            sw.Close();
            fs.Close();
        }
        public void montagemBloco0150()
        {
            FileStream fs = new FileStream(@caminhoArquivo + @"\sped" + dtIni + "a" + dtFin + ".txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            for (int i = 0; i < participantes.Count; i++)
            {
                sw.WriteLine("|0150|" + campoFormatado(((Participante)participantes[i]).codPart)+campoFormatado(((Participante)participantes[i]).razaosocial)+
                    campoFormatado(((Participante)participantes[i]).codPais)+campoFormatado(((Participante)participantes[i]).cnpj)+campoFormatado(((Participante)participantes[i]).CPF)+
                    campoFormatado(((Participante)participantes[i]).IE)+campoFormatado(((Participante)participantes[i]).codMun)+"|"+campoFormatado(((Participante)participantes[i]).logradouro)+
                    campoFormatado(((Participante)participantes[i]).numero)+campoFormatado(((Participante)participantes[i]).complemento)+campoFormatado(((Participante)participantes[i]).bairro));
                registros.Add(setRegistro("0150"));
            }
            sw.Close();
            fs.Close();
        }
        public void montagemBloco0190()
        {
            FileStream fs = new FileStream(@caminhoArquivo + @"\sped" + dtIni + "a" + dtFin + ".txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            for (int i = 0; i < unidades.Count; i++)
            {
                sw.WriteLine("|0190|" + campoFormatado(((Unidade)unidades[i]).sigla) + campoFormatado(((Unidade)unidades[i]).sigla + " UNIDADE"));
                registros.Add(setRegistro("0190"));
            }
            
            sw.Close();
            fs.Close();
        }
        public void montagemBloco0200()
        {
            FileStream fs = new FileStream(@caminhoArquivo + @"\sped" + dtIni + "a" + dtFin + ".txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            for (int i = 0; i < produtos.Count; i++)
            {
                sw.WriteLine("|0200|" + campoFormatado(((Produto)produtos[i]).codigo) + campoFormatado(((Produto)produtos[i]).descricao)+campoFormatado(((Produto)produtos[i]).codbarra)+
                    campoFormatado("")+campoFormatado(((Produto)produtos[i]).unid)+campoFormatado("00")+campoFormatado(((Produto)produtos[i]).NCM)+"|||||");
                registros.Add(setRegistro("0200"));
            }
            
            sw.Close();
            fs.Close();
        }
        public void montagemBloco0400()
        {
            FileStream fs = new FileStream(@caminhoArquivo + @"\sped" + dtIni + "a" + dtFin + ".txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            for (int i = 0; i < CFOP.Count; i++)
            {
                sw.WriteLine("|0400|" + campoFormatado(((CFOP)CFOP[i]).codigo) + campoFormatado(((CFOP)CFOP[i]).codigo + " CFOP"));
                registros.Add(setRegistro("0400"));
            }
            registros.Add(setRegistro("0990"));
            string NumRegistros = Convert.ToString(getQtdRegistro("0000") + getQtdRegistro("0001") + getQtdRegistro("0005") + getQtdRegistro("0100") + getQtdRegistro("0150") + getQtdRegistro("0190") +
                getQtdRegistro("0200") + getQtdRegistro("0400")+1);
            sw.WriteLine("|0990|"+campoFormatado(NumRegistros));
            sw.Close();
            fs.Close();
        }
        public void montagemBlocoC()
        {
            FileStream fs = new FileStream(@caminhoArquivo + @"\sped" + dtIni + "a" + dtFin + ".txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(aberturaBlocoCheio("C001"));
            for (int i = 0; i < notas.Count; i++)
            {
                sw.WriteLine(montagemBlocoC100(((Nota)notas[i])));
                registros.Add(setRegistro("C100"));
                if (((Nota)notas[i]).indOper.Equals("0"))
                {
                    for (int j = 0; j < ((Nota)notas[i]).itensNota.Count; j++)
                    {
                        sw.WriteLine(montagemBlocoC170(((Nota)notas[i]), j));
                        registros.Add(setRegistro("C170"));
                    }
                }
                for(int k=0;k<((Nota)notas[i]).regAnaliticos.Count; k++)
                {
                    sw.WriteLine(montagemBlocoC190(((Nota)notas[i]),k));
                    registros.Add(setRegistro("C190"));
                }   
            }
            registros.Add(setRegistro("C990"));
            sw.WriteLine("|C990|" + campoFormatado(Convert.ToString(getQtdRegistro("C990") + getQtdRegistro("C001")+getQtdRegistro("C100")+getQtdRegistro("C170")+getQtdRegistro("C190"))));
            sw.Close();
            fs.Close();
        }
        public void montagemBlocoVazio(string nomeBloco)
        {

            FileStream fs = new FileStream(@caminhoArquivo + @"\sped" + dtIni + "a" + dtFin + ".txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            registros.Add(setRegistro(nomeBloco + "001"));
            sw.WriteLine("|" + campoFormatado(nomeBloco + "001") + "1|");
            registros.Add(setRegistro(nomeBloco + "990"));
            sw.WriteLine("|" + campoFormatado(nomeBloco + "990") + "2|");
            sw.Close();
            fs.Close();
        }
        public string montagemBlocoC100(Nota nota)
        {
            string bloco;
            bloco = "|C100|" + campoFormatado(nota.indOper) + campoFormatado(nota.indEmit) + campoFormatado(nota.codPart) + campoFormatado(nota.codMod) + campoFormatado("00") + 
                campoFormatado(nota.serie) + campoFormatado(nota.numNota) + campoFormatado(nota.chaveNota) + campoFormatado(nota.dtEmissao) + campoFormatado(nota.dtEmissao) +
                campoFormatado(nota.vlDoc) + campoFormatado("2") + campoFormatado(nota.vlDesc) + "0|" + campoFormatado(nota.vlMerc) + campoFormatado(nota.indFrete) + campoFormatado(nota.vlFrete) +
                campoFormatado(nota.vlSeg) + campoFormatado(nota.vlOutrasDespesas) + campoFormatado(nota.vlBCICMS) + campoFormatado(nota.vlICMS) + campoFormatado(nota.vlBCICMSST) + campoFormatado(nota.vlICMSST) +
                campoFormatado(nota.vlIPI) + campoFormatado(nota.vlPIS) + campoFormatado(nota.vlCOFINS) + campoFormatado(nota.vlPISST) + campoFormatado(nota.vlCOFINSST);
            return bloco;

        }
        public string montagemBlocoC170(Nota nota, int numItem)
        {
            string bloco;
            bloco = "|C170|" + campoFormatado(Convert.ToString(numItem+1)) + campoFormatado(((itemNota)nota.itensNota[numItem]).codItem) + campoFormatado(((itemNota)nota.itensNota[numItem]).descItem) + campoFormatado(((itemNota)nota.itensNota[numItem]).qtd) +
                 campoFormatado(((itemNota)nota.itensNota[numItem]).unid) + campoFormatado(((itemNota)nota.itensNota[numItem]).vlItem) + campoFormatado(((itemNota)nota.itensNota[numItem]).vlDesc) + "0|" + campoFormatado(((itemNota)nota.itensNota[numItem]).cstICMS) +
                 campoFormatado(((itemNota)nota.itensNota[numItem]).CFOP) + campoFormatado(((itemNota)nota.itensNota[numItem]).CFOP) + campoFormatado(((itemNota)nota.itensNota[numItem]).vlBCICMS) + campoFormatado(((itemNota)nota.itensNota[numItem]).aliqICMS) +
                 campoFormatado(((itemNota)nota.itensNota[numItem]).vlICMS) + campoFormatado(((itemNota)nota.itensNota[numItem]).vlBCICMSST) + campoFormatado(((itemNota)nota.itensNota[numItem]).aliqST) + campoFormatado(((itemNota)nota.itensNota[numItem]).vlICMSST) +
                 campoFormatado("0") + campoFormatado(((itemNota)nota.itensNota[numItem]).CSTIPI) + campoFormatado(((itemNota)nota.itensNota[numItem]).codEnq) + campoFormatado(((itemNota)nota.itensNota[numItem]).vlBCIPI) + campoFormatado(((itemNota)nota.itensNota[numItem]).aliqIPI) +
                 campoFormatado(((itemNota)nota.itensNota[numItem]).vlIPI) + campoFormatado(((itemNota)nota.itensNota[numItem]).cstPIS) + campoFormatado(((itemNota)nota.itensNota[numItem]).vlBCPIS) + campoFormatado(((itemNota)nota.itensNota[numItem]).aliqPIS) +
                 campoFormatado(((itemNota)nota.itensNota[numItem]).qtdBCPIS) + campoFormatado(((itemNota)nota.itensNota[numItem]).aliqPIS) + campoFormatado(((itemNota)nota.itensNota[numItem]).vlPIS) + campoFormatado(((itemNota)nota.itensNota[numItem]).CSTCOFINS) +
                 campoFormatado(((itemNota)nota.itensNota[numItem]).vlBCCOFINS) + campoFormatado(((itemNota)nota.itensNota[numItem]).aliqCOFINS) + campoFormatado(((itemNota)nota.itensNota[numItem]).qtdBCCOFINS) + campoFormatado(((itemNota)nota.itensNota[numItem]).aliqCOFINS) +
                 campoFormatado(((itemNota)nota.itensNota[numItem]).vlCOFINS) + campoFormatado("") + campoFormatado("");
            return bloco;
        }
        public string montagemBlocoC190(Nota nota, int numReg)
        {
            string bloco;
            bloco = "|C190|" + campoFormatado(((regAnalitico)nota.regAnaliticos[numReg]).cstICMS) + campoFormatado(((regAnalitico)nota.regAnaliticos[numReg]).CFOP) + campoFormatado(((regAnalitico)nota.regAnaliticos[numReg]).aliqICMS) +
                campoFormatado(Convert.ToString(((regAnalitico)nota.regAnaliticos[numReg]).vlOper)) + campoFormatado(Convert.ToString(((regAnalitico)nota.regAnaliticos[numReg]).vlBCICMS))+
                campoFormatado(Convert.ToString(((regAnalitico)nota.regAnaliticos[numReg]).vlICMS)) + campoFormatado(Convert.ToString(((regAnalitico)nota.regAnaliticos[numReg]).vlBCICMSST))+
                campoFormatado(Convert.ToString(((regAnalitico)nota.regAnaliticos[numReg]).vlICMSST)) + campoFormatado("0") + campoFormatado(Convert.ToString(((regAnalitico)nota.regAnaliticos[numReg]).vlIPI))+"|";
            return bloco;
        }
        public void montagemBloco9()
        {
            FileStream fs = new FileStream(@caminhoArquivo + @"\sped" + dtIni + "a" + dtFin + ".txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(aberturaBlocoCheio("9001"));
            sw.WriteLine(getRegistros9900("0000"));
            sw.WriteLine(getRegistros9900("0001"));
            sw.WriteLine(getRegistros9900("0005"));
            sw.WriteLine(getRegistros9900("0100"));
            sw.WriteLine(getRegistros9900("0150"));
            sw.WriteLine(getRegistros9900("0190"));
            sw.WriteLine(getRegistros9900("0200"));
            sw.WriteLine(getRegistros9900("0400"));
            sw.WriteLine(getRegistros9900("0990"));
            sw.WriteLine(getRegistros9900("B001"));
            sw.WriteLine(getRegistros9900("B990"));
            sw.WriteLine(getRegistros9900("C001"));
            sw.WriteLine(getRegistros9900("C100"));
            sw.WriteLine(getRegistros9900("C170"));
            sw.WriteLine(getRegistros9900("C190"));
            sw.WriteLine(getRegistros9900("C990"));
            sw.WriteLine(getRegistros9900("D001"));
            sw.WriteLine(getRegistros9900("D990"));
            sw.WriteLine(getRegistros9900("E001"));
            sw.WriteLine(getRegistros9900("E990"));
            sw.WriteLine(getRegistros9900("G001"));
            sw.WriteLine(getRegistros9900("G990"));
            sw.WriteLine(getRegistros9900("H001"));
            sw.WriteLine(getRegistros9900("H990"));
            sw.WriteLine(getRegistros9900("K001"));
            sw.WriteLine(getRegistros9900("K990"));
            sw.WriteLine(getRegistros9900("1001"));
            sw.WriteLine(getRegistros9900("1990"));
            sw.WriteLine(getRegistros9900("9001"));
            registros.Add(setRegistro("9900"));
            registros.Add(setRegistro("9990"));
            registros.Add(setRegistro("9999"));
            sw.WriteLine(getRegistros9900("9990"));
            sw.WriteLine(getRegistros9900("9999"));
            sw.WriteLine(getRegistros9900("9900"));
            sw.WriteLine("|9990|" + Convert.ToString(getQtdRegistro("9001") + getQtdRegistro("9900") + getQtdRegistro("9990")) + "|");
            sw.WriteLine("|9999|" + campoFormatado(Convert.ToString(qtdLinhas-1)));
            sw.Close();
            fs.Close();
        }
        public string getRegistros9900(string registro)
        {
            string reg;
            reg = "|9900|"+registro+"|" + campoFormatado(Convert.ToString(getQtdRegistro(registro)));
            registros.Add(setRegistro("9900"));
            return reg;
        }
        public void setProdutos()
        {
            

            for (int i = 0; i < notas.Count; i++)
            {
                if (((Nota)notas[i]).codMod.Equals("55") && ((Nota)notas[i]).indOper.Equals("0"))
                {
                    for (int j = 0; j < ((Nota)notas[i]).itensNota.Count; j++)
                    {
                        int cont = 0;
                        if (i == 0 && j == 0)
                        {
                            produtos.Add(popularProduto((itemNota)((Nota)notas[i]).itensNota[j]));
                            cont++;
                        }
                        else
                        {
                            for (int l = 0; l < produtos.Count; l++)
                            {
                                if (((itemNota)((Nota)notas[i]).itensNota[j]).codItem.Equals(((Produto)produtos[l]).codigo))
                                {
                                    cont++;
                                }
                            }
                        }
                        if (cont == 0)
                        {
                            produtos.Add(popularProduto((itemNota)((Nota)notas[i]).itensNota[j]));
                        }
                    }
                }

            }
        }
        public void setCFOP()
        {


            for (int i = 0; i < notas.Count; i++)
            {
                if (((Nota)notas[i]).codMod.Equals("55") && ((Nota)notas[i]).indOper.Equals("0"))
                {
                    if (((Nota)notas[i]).codMod.Equals("55") && ((Nota)notas[i]).indOper.Equals("0"))
                    {
                        for (int j = 0; j < ((Nota)notas[i]).itensNota.Count; j++)
                        {
                            int cont = 0;
                            if (i == 0 && j == 0)
                            {
                                CFOP.Add(popularCFOP((itemNota)((Nota)notas[i]).itensNota[j]));
                                cont++;
                            }
                            else
                            {
                                for (int l = 0; l < CFOP.Count; l++)
                                {
                                    if (((itemNota)((Nota)notas[i]).itensNota[j]).CFOP.Equals(((CFOP)CFOP[l]).codigo))
                                    {
                                        cont++;
                                    }
                                }
                            }
                            if (cont == 0)
                            {
                                CFOP.Add(popularCFOP((itemNota)((Nota)notas[i]).itensNota[j]));
                            }
                        }
                    }

                }
            }
        }

        public string campoFormatado(string Campo)
        {
            return Campo + "|";
        }

        public void setParticipantes()
        {
            for (int i = 0; i < notas.Count; i++)
            {
                int cont = 0;
                if(((Nota)notas[i]).participante!=null)
                {
                    if (i == 0)
                    {
                        participantes.Add(((Nota)notas[i]).participante);
                    }
                    else
                    {
                        for (int j = 0; j < participantes.Count; j++)
                        {
                            //if (((Participante)notas[i].participante).codPart.Equals(((Participante)participantes[j]).cnpj))
                            if(((Participante)((Nota)notas[i]).participante).codPart.Equals(((Participante)participantes[j]).codPart))
                            {
                                cont++;
                            }
                        }
                        if (cont == 0)
                        {
                            participantes.Add(((Nota)notas[i]).participante);
                        }
                    }
                }
            }
        }
        public Participante setParticipante(XmlDocument doc)
        {
            XmlNodeList nos = doc.GetElementsByTagName("emit");
            Participante part = new Participante();
            XmlNode no = nos[0];
            for (int i = 0; i<no.ChildNodes.Count; i++)
            {
                if (no.ChildNodes[i].Name.Equals("CNPJ"))
                {
                    /* if (no.ChildNodes[i]==null)
                     {
                         XmlNodeList nosCPF = doc.GetElementsByTagName("CPF");
                         XmlNode noCPF = nosCPF[0];
                         part.cnpj = noCPF.InnerText;
                     }
                     else*/
                    part.cnpj = no.ChildNodes[i].InnerText;
                    part.CPF = "";
                    part.codPart = part.cnpj;
                }
                else if (no.ChildNodes[i].Name.Equals("CPF"))
                {
                    part.cnpj = no.ChildNodes[i].InnerText;
                    part.cnpj = "";
                    part.IE = "";
                    part.codPart = part.CPF;
                }

                else if (no.ChildNodes[i].Name.Equals("xNome"))
                {
                    part.razaosocial = no.ChildNodes[i].InnerText;
                }
                else if (no.ChildNodes[i].Name.Equals("CPF"))
                {
                    part.CPF = no.ChildNodes[i].InnerText;
                }
                else if (no.ChildNodes[i].Name.Equals("IE"))
                {
                    part.IE = no.ChildNodes[i].InnerText;
                }

                else if (no.ChildNodes[i].Name.Equals("enderEmit"))
                {
                    for (int j = 0; j < no.ChildNodes[i].ChildNodes.Count; j++)
                    {
                        if (no.ChildNodes[i].ChildNodes[j].Name.Equals("cPais"))
                        {
                            part.codPais = no.ChildNodes[i].ChildNodes[j].InnerText;
                        }
                        if (no.ChildNodes[i].ChildNodes[j].Name.Equals("xPais"))
                        {
                            if (no.ChildNodes[i].ChildNodes[j].InnerText.Equals("Brasil") || no.ChildNodes[i].ChildNodes[j].InnerText.Equals("BRASIL"))
                            {
                                part.codPais = "1058";
                            }
                        }
                        else if (no.ChildNodes[i].ChildNodes[j].Name.Equals("cMun"))
                        {
                            part.codMun = no.ChildNodes[i].ChildNodes[j].InnerText;
                        }
                        else if (no.ChildNodes[i].ChildNodes[j].Name.Equals("xLgr"))
                        {
                            part.logradouro = no.ChildNodes[i].ChildNodes[j].InnerText;
                        }
                        else if (no.ChildNodes[i].ChildNodes[j].Name.Equals("nro"))
                        {
                            part.numero = no.ChildNodes[i].ChildNodes[j].InnerText;
                        }
                        else if (no.ChildNodes[i].ChildNodes[j].Name.Equals("xCpl"))
                        {
                            if (no.ChildNodes[i].ChildNodes[j] == null)
                            {
                                part.complemento = "";
                            }
                            else

                                part.complemento = no.ChildNodes[i].ChildNodes[j].InnerText;
                        }
                        else if (no.ChildNodes[i].ChildNodes[j].Name.Equals("xBairro"))
                        {
                            part.bairro = no.ChildNodes[i].ChildNodes[j].InnerText;
                        }
                    }

                }
            }
                
            return part;
        }
        public Participante setParticipanteDest(XmlDocument doc)
        {
            XmlNodeList nos = doc.GetElementsByTagName("dest");
            Participante part = new Participante();
            XmlNode no = nos[0];
            for (int i = 0; i < no.ChildNodes.Count; i++)
            {
                if (no.ChildNodes[i].Name.Equals("CNPJ"))
                {
                    part.cnpj = no.ChildNodes[i].InnerText;
                    part.CPF = "";
                    part.codPart = part.cnpj;
                }
                else if (no.ChildNodes[i].Name.Equals("CPF"))
                {
                    part.CPF = no.ChildNodes[i].InnerText;
                    part.cnpj = "";
                    part.IE = "";
                    part.codPart = part.CPF;
                    
                }
                else if (no.ChildNodes[i].Name.Equals("xNome"))
                {
                    part.razaosocial = no.ChildNodes[i].InnerText;
                }
                else if (no.ChildNodes[i].Name.Equals("CPF"))
                {
                    part.CPF = no.ChildNodes[i].InnerText;
                }
                else if (no.ChildNodes[i].Name.Equals("IE"))
                {
                    part.IE = no.ChildNodes[i].InnerText;
                }

                else if (no.ChildNodes[i].Name.Equals("enderDest"))
                {
                    for (int j = 0; j < no.ChildNodes[i].ChildNodes.Count; j++)
                    {
                        if (no.ChildNodes[i].ChildNodes[j].Name.Equals("cPais"))
                        {
                            part.codPais = no.ChildNodes[i].ChildNodes[j].InnerText;
                        }
                        else if (no.ChildNodes[i].ChildNodes[j].Name.Equals("cMun"))
                        {
                            part.codMun = no.ChildNodes[i].ChildNodes[j].InnerText;
                        }
                        else if (no.ChildNodes[i].ChildNodes[j].Name.Equals("xLgr"))
                        {
                            part.logradouro = no.ChildNodes[i].ChildNodes[j].InnerText;
                        }
                        else if (no.ChildNodes[i].ChildNodes[j].Name.Equals("nro"))
                        {
                            part.numero = no.ChildNodes[i].ChildNodes[j].InnerText;
                        }
                        else if (no.ChildNodes[i].ChildNodes[j].Name.Equals("xCpl"))
                        {
                            if (no.ChildNodes[i].ChildNodes[j] == null)
                            {
                                part.complemento = "";
                            }
                            else

                                part.complemento = no.ChildNodes[i].ChildNodes[j].InnerText;
                        }
                        else if (no.ChildNodes[i].ChildNodes[j].Name.Equals("xBairro"))
                        {
                            part.bairro = no.ChildNodes[i].ChildNodes[j].InnerText;
                        }
                    }
                }
            }

                

            return part;
 
        }
        public Produto popularProduto(itemNota itemNota)
        {
            Produto prod = new Produto();
            prod.codigo = itemNota.codItem;
            prod.descricao= itemNota.descItem;
            prod.unid = itemNota.unid;
            prod.NCM = itemNota.NCM;
            return prod;
        }
        public CFOP popularCFOP(itemNota itemNota)
        {
            {
                CFOP cfop = new CFOP();
                cfop.codigo = itemNota.CFOP;
                
                return cfop;
            }
        }

        

        public void setUnidades ()
        {
            DirectoryInfo diretorio = new DirectoryInfo(caminhoXml);
            FileInfo[] Arquivos = diretorio.GetFiles("*.xml");
            XmlDocument doc = new XmlDocument();
            
            for (int i = 0; i < Arquivos.Count(); i++)
            {
                Unidade unid = new Unidade();
                doc.Load(@caminhoXml + @"\" + Arquivos[i].Name);
                XmlNodeList nos = doc.GetElementsByTagName("mod");
                string modelo = nos[0].InnerText;
                nos = doc.GetElementsByTagName("prod");
                if (i == 0 && modelo.Equals("55"))
                {
                   
                    for (int j = 0; j < nos.Count; j++)
                    {

                        for (int k = 0; k < nos[j].ChildNodes.Count; k++)
                        {
                            if (nos[j].ChildNodes[k].Name.Equals("uCom"))
                            {
                                unid.sigla = nos[j].ChildNodes[k].InnerText;
                                unidades.Add(unid);
                            }
                        }
                    }
                }
                if (i > 0 && modelo.Equals("55"))
                {
                    for (int j = 0; j < nos.Count; j++)
                    {

                        for (int k = 0; k < nos[j].ChildNodes.Count; k++)
                        {
                            if (nos[j].ChildNodes[k].Name.Equals("uCom"))
                            {
                                unid.sigla = nos[j].ChildNodes[k].InnerText;
                                int cont = 0;
                                for (int l = 0; l < unidades.Count; l++)
                                {
                                    if (unid.sigla.Equals(((Unidade)unidades[l]).sigla))
                                    {
                                        cont++;
                                    }
                                }
                                if (cont == 0)
                                {
                                    unidades.Add(unid);
                                }
                            }
                        }
                    }
                }
            }

        }
        public void setNotas()
        {
            DirectoryInfo diretorio = new DirectoryInfo(caminhoXml);
            FileInfo[] Arquivos = diretorio.GetFiles("*.xml");
            XmlDocument doc = new XmlDocument();
            for (int i = 0; i < Arquivos.Count(); i++)
            {
                doc.Load(@caminhoXml + @"\" + Arquivos[i].Name);
                Nota nota = new Nota();
                XmlNodeList nos = doc.GetElementsByTagName("tpNF");
                string tpNF = nos[0].InnerText;
                nos = doc.GetElementsByTagName("mod");
                nota.codMod = nos[0].InnerText;
                nos = doc.GetElementsByTagName("CNPJ");
                nota.CRT = CRT;
                if (nos[0].InnerText.Equals(cnpj))
                {
                    nota.indOper = tpNF;
                    nota.indEmit = "0";
                }
                else
                {
                    nota.indOper = "0";
                    nota.indEmit = "1";
                }
                if (nota.codMod.Equals("55") && nota.indOper.Equals("0"))
                {
                    nota.codPart = nos[0].InnerText;
                }
                else if (nota.codMod.Equals("55") && nota.indOper.Equals("1"))
                {
                    nos = doc.GetElementsByTagName("dest");
                    {
                        for (int l = 0; l < nos[0].ChildNodes.Count; l++)
                        {
                            if (nos[0].ChildNodes[l].Name.Equals("CNPJ"))
                            {
                                nota.codPart = nos[0].ChildNodes[l].InnerText;
                            }
                            else if (nos[0].ChildNodes[l].Name.Equals("CPF"))
                            {
                                nota.codPart = nos[0].ChildNodes[l].InnerText;
                            }
                        }
                    }
                }
                //nota.codPart = nos[0].InnerText;
                if (nota.indOper.Equals("0") && nota.codMod.Equals("55"))
                {
                    nota.participante = nota.popularparticipante("emit", doc);
                }
                if (nota.indOper.Equals("1")&&nota.codMod.Equals("55"))
                {
                    nota.participante = nota.popularparticipante("dest", doc);
                }
                nos = doc.GetElementsByTagName("serie");
                
                nota.serie = nos[0].InnerText;
                nos = doc.GetElementsByTagName("nNF");
                nota.numNota = nos[0].InnerText;
                nos = doc.GetElementsByTagName("infNFe");
                nota.chaveNota = nos[0].Attributes["Id"].InnerText.Replace("NFe", "");
                nos = doc.GetElementsByTagName("dhEmi");
                DateTime dt = DateTime.ParseExact(nos[0].InnerText.Substring(0, 10).Replace("-", ""), "yyyyMMdd", CultureInfo.InvariantCulture);
                nota.dtEmissao = dt.ToString("ddMMyyyy");
                nos = doc.GetElementsByTagName("vNF");
                nota.vlDoc = nos[0].InnerText.Replace(".", ",");
                nota.indPgto = "2";
                nos = doc.GetElementsByTagName("ICMSTot");
                    for (int j = 0; j < nos[0].ChildNodes.Count; j++)
                    {
                        if (nos[0].ChildNodes[j].Name.Equals("vDesc"))
                        {
                            nota.vlDesc = nos[0].ChildNodes[j].InnerText.Replace(".", ",");
                        }
                        else if (nos[0].ChildNodes[j].Name.Equals("vProd"))
                        {
                            nota.vlMerc = nos[0].ChildNodes[j].InnerText.Replace(".", ",");
                        }
                        else if (nos[0].ChildNodes[j].Name.Equals("vOutro"))
                        {
                            nota.vlOutrasDespesas = nos[0].ChildNodes[j].InnerText.Replace(".", ",");
                        }
                        else if (nos[0].ChildNodes[j].Name.Equals("vBC"))
                        {
                            nota.vlBCICMS = nos[0].ChildNodes[j].InnerText.Replace(".", ",");
                        }
                        else if (nos[0].ChildNodes[j].Name.Equals("vICMS"))
                        {
                            nota.vlICMS = nos[0].ChildNodes[j].InnerText.Replace(".", ",");
                        }
                        else if (nos[0].ChildNodes[j].Name.Equals("vBCST"))
                        {
                            nota.vlBCICMSST= nos[0].ChildNodes[j].InnerText.Replace(".", ",");
                            if (nota.codMod.Equals("65"))
                            {
                                nota.vlBCICMSST = "";
                            }
                        }
                        else if (nos[0].ChildNodes[j].Name.Equals("vST"))
                        {
                            nota.vlICMSST = nos[0].ChildNodes[j].InnerText.Replace(".", ",");
                            if (nota.codMod.Equals("65"))
                            {
                                nota.vlICMSST = "";
                            }
                        }
                        else if (nos[0].ChildNodes[j].Name.Equals("vIPI"))
                        {
                            nota.vlIPI = nos[0].ChildNodes[j].InnerText.Replace(".", ",");
                            if (nota.codMod.Equals("65"))
                            {
                                nota.vlIPI = "";
                            }
                        }
                        else if (nos[0].ChildNodes[j].Name.Equals("vPIS"))
                        {
                            nota.vlPIS = nos[0].ChildNodes[j].InnerText.Replace(".", ",");
                            if (nota.codMod.Equals("65"))
                            {
                                nota.vlPIS = "";
                            }
                        }
                        else if (nos[0].ChildNodes[j].Name.Equals("vCOFINS"))
                        {
                            nota.vlCOFINS = nos[0].ChildNodes[j].InnerText.Replace(".", ",");
                            if (nota.codMod.Equals("65"))
                            {
                                nota.vlCOFINS = "";
                            }
                        }
                        else if (nos[0].ChildNodes[j].Name.Equals("vCOFINSST"))
                        {
                            nota.vlCOFINSST = nos[0].ChildNodes[j].InnerText.Replace(".", ",");
                            
                            if (nota.codMod.Equals("65"))
                            {
                                nota.vlCOFINSST = "";
                            }
                        }
                        else if (nos[0].ChildNodes[j].Name.Equals("vPISST"))
                        {
                            nota.vlPISST = nos[0].ChildNodes[j].InnerText.Replace(".", ",");
                        }
                        
                    }
                    nos = doc.GetElementsByTagName("modFrete");
                    nota.indFrete = nos[0].InnerText;
                    nota.vlFrete = "0";
                    nota.vlSeg = "0";
                    if (nota.codMod.Equals("65"))
                    {
                        nota.vlPISST = "";
                    }
                    else
                    {
                        nota.vlPISST = "0";
                    }
                    if (nota.codMod.Equals("65"))
                    {
                        nota.vlCOFINSST = "";
                    }
                    else
                    {
                        nota.vlCOFINSST = "0";
                    } 
                    nota.setItem(doc);
                    nota.setRegAnaliticos();

                    notas.Add(nota);
            }
        }
        public Registro setRegistro(string nomeReg)
        {
            Registro reg = new Registro();
            reg.nome = nomeReg;
            qtdLinhas++;
            return reg;
        }
        public int getQtdRegistro(string nomeReg)
        {
            int count = 0;
            for (int i = 0; i < registros.Count; i++)
            {
                if (((Registro)registros[i]).nome.Equals(nomeReg))
                {
                    count++;
                }
            }
            return count;
        }
        public string aberturaBlocoCheio(string nomeReg)
        {
            registros.Add(setRegistro(nomeReg));
            return "|" + nomeReg + "|0|";
        }
        /*public string fecahmentoBlocoCheio(string nomeReg)
        {
            return "|" + nomeReg + "|"+getQtdRegistro(nomeReg)+"|";
        }
         */ 
    }
    
}
