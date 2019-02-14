using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace simpleSpedNFCE_2._0
{
    class Nota
    {
        public string indOper;
        public string indEmit;
        public string codPart;
        public string codMod;
        public string serie;
        public string numNota;
        public string chaveNota;
        public string dtEmissao;
        public string dtEs;
        public string vlDoc;
        public string indPgto;
        public string vlDesc;
        public string vlMerc;
        public string indFrete;
        public string vlFrete;
        public string vlSeg;
        public string vlOutrasDespesas;
        public string vlBCICMS;
        public string vlICMS;
        public string vlBCICMSST;
        public string vlICMSST;
        public string vlIPI;
        public string vlPIS;
        public string vlCOFINS;
        public string vlPISST;
        public string vlCOFINSST;
        public string CRT;
        public ArrayList itensNota = new ArrayList();
        public ArrayList regAnaliticos = new ArrayList();
        public Participante participante;

        public void setItem(XmlDocument doc)
        {
            XmlNodeList nos = doc.GetElementsByTagName("prod");

            for (int i = 0; i < nos.Count; i++)
            {
                itemNota itemNota = new itemNota();
                itemNota.vlDesc = "0";
                for (int j = 0; j < nos[i].ChildNodes.Count; j++)
                {
                    if (nos[i].ChildNodes[j].Name.Equals("cProd"))
                    {
                        itemNota.codItem = nos[i].ChildNodes[j].InnerText.Replace(".", ",");
                    }
                    else if (nos[i].ChildNodes[j].Name.Equals("xProd"))
                    {
                        itemNota.descItem = nos[i].ChildNodes[j].InnerText.Replace(".", ",");
                    }
                    else if (nos[i].ChildNodes[j].Name.Equals("qCom"))
                    {
                        itemNota.qtd = nos[i].ChildNodes[j].InnerText.Replace(".", ",");
                        itemNota.qtdBCPIS = itemNota.qtd;
                        itemNota.qtdBCCOFINS = itemNota.qtd;
                        if (itemNota.qtd.Length > 5)
                        {
                            itemNota.qtdBCPIS = itemNota.qtd.Substring(0, 5);
                            itemNota.qtdBCCOFINS = itemNota.qtd.Substring(0, 5);
                        }
                    }
                    else if (nos[i].ChildNodes[j].Name.Equals("vProd"))
                    {
                        itemNota.vlItem = nos[i].ChildNodes[j].InnerText.Replace(".", ",");
                    }
                    else if (nos[i].ChildNodes[j].Name.Equals("vDesc"))
                    {
                        itemNota.vlDesc = nos[i].ChildNodes[j].InnerText.Replace(".", ",");
                    }
                    else if (nos[i].ChildNodes[j].Name.Equals("CFOP"))
                    {
                        itemNota.CFOP = nos[i].ChildNodes[j].InnerText.Replace(".", ",");
                    }
                    else if (nos[i].ChildNodes[j].Name.Equals("NCM"))
                    {
                        itemNota.NCM = nos[i].ChildNodes[j].InnerText.Replace(".", ",");
                    }
                    else if (nos[i].ChildNodes[j].Name.Equals("uCom"))
                    {
                        itemNota.unid = nos[i].ChildNodes[j].InnerText.Replace(".", ",");
                    }
                    XmlNodeList nosICMS = doc.GetElementsByTagName("ICMS");
                    itemNota.cstICMS = getCSTCSOSN(nosICMS, doc, i);

                    /*nosICMS = doc.GetElementsByTagName("vBC");
                    itemNota.vlBCICMS = nosICMS[i].InnerText;
                    nosICMS = doc.GetElementsByTagName("vBC");
                    itemNota.vlBCICMS = nosICMS[i].InnerText;*/
                    itemNota.vlBCICMS = getDadosICMS(nosICMS, doc, i, "vBC").Replace(".", ",");
                    itemNota.aliqICMS = getDadosICMS(nosICMS, doc, i, "pICMS").Replace(".", ",");
                    if (itemNota.aliqICMS.Length > 4)
                    {
                        itemNota.aliqICMS = itemNota.aliqICMS.Substring(0, 4);
                    }
                    itemNota.vlICMS = getDadosICMS(nosICMS, doc, i, "vICMS").Replace(".", ",");
                    itemNota.vlBCICMSST = getDadosICMS(nosICMS, doc, i, "vBCST").Replace(".", ",");
                    itemNota.aliqST = getDadosICMS(nosICMS, doc, i, "pICMSST").Replace(".", ",");
                    itemNota.vlICMSST = getDadosICMS(nosICMS, doc, i, "vICMSST").Replace(".", ",");
                    if (codMod.Equals("55"))
                    {
                        nosICMS = doc.GetElementsByTagName("IPI");
                        itemNota.codEnq = "";
                        itemNota.CSTIPI = getDadosIPI(nosICMS, i, "CST");
                        itemNota.vlBCIPI = getDadosIPI(nosICMS, i, "vBC");
                        itemNota.aliqIPI = getDadosIPI(nosICMS, i, "pIPI");
                        itemNota.vlIPI = getDadosIPI(nosICMS, i, "vIPI");
                        if (getDadosIPI(nosICMS, i, "vIPI").Equals(""))
                        {
                            itemNota.vlIPI = "0";
                        }
                    }
                    else
                    {
                        itemNota.codEnq = "";
                        itemNota.CSTIPI = "";
                        itemNota.vlBCIPI = "0";
                        itemNota.vlBCIPI = "0";
                        itemNota.aliqIPI = "0";
                        itemNota.vlIPI = "0";
                    }
                    
                    nosICMS = doc.GetElementsByTagName("PIS");
                    if (CRT.Equals("2"))
                    {
                        itemNota.cstPIS = "07";
                    }
                    else
                    {
                        itemNota.cstPIS = getDadosICMS(nosICMS, doc, i, "CST");
                    }
                        itemNota.vlBCPIS = getDadosICMS(nosICMS, doc, i, "vBC").Replace(".",",");
                    itemNota.aliqPIS = getDadosICMS(nosICMS, doc, i, "pPIS").Replace(".",",");
                    itemNota.vlPIS = getDadosICMS(nosICMS, doc, i, "vPIS").Replace(".", ",");

                    nosICMS = doc.GetElementsByTagName("COFINS");
                    if (CRT.Equals("2"))
                    {
                        itemNota.CSTCOFINS = "07";
                    }
                    else
                    {
                        itemNota.CSTCOFINS = getDadosICMS(nosICMS, doc, i, "CST");
                    } 
                    itemNota.vlBCCOFINS = getDadosICMS(nosICMS, doc, i, "vBC").Replace(".", ",");
                    itemNota.aliqCOFINS = getDadosICMS(nosICMS, doc, i, "pCOFINS").Replace(".", ",");
                    itemNota.vlCOFINS = getDadosICMS(nosICMS, doc, i, "vCOFINS").Replace(".", ",");
                    
                }
                itensNota.Add(itemNota);
               



            }
        }
        public string getCSTCSOSN(XmlNodeList nos, XmlDocument doc, int i)
        {
            string CSTCSOSN="0";
            XmlNodeList nosOrig = doc.GetElementsByTagName("orig");
            string origem = nosOrig[i].InnerText;
            XmlNodeList nosCRT = doc.GetElementsByTagName("CRT");
            if (nosCRT[0].InnerText.Equals("1") || nosCRT[0].InnerText.Equals("2"))
            {
                for (int j = 0; j < nos[i].ChildNodes[0].ChildNodes.Count; j++)
                {
                    if (nos[i].ChildNodes[0].ChildNodes[j].Name.Equals("CSOSN"))
                        CSTCSOSN = nos[i].ChildNodes[0].ChildNodes[j].InnerText;
                }
                    
            }
            else
            {
                for (int j = 0; j < nos[i].ChildNodes[0].ChildNodes.Count; j++)
                {
                    if (nos[i].ChildNodes[0].ChildNodes[j].Name.Equals("CST"))
                        CSTCSOSN = origem + nos[i].ChildNodes[0].ChildNodes[j].InnerText;
                }
            }
            return CSTCSOSN;
        }
        public string getDadosICMS(XmlNodeList nos, XmlDocument doc, int i, string Campo)
        {
            string DadosICMS = "0";
            for (int j = 0; j < nos[i].ChildNodes[0].ChildNodes.Count; j++)
            {
                if (nos[i].ChildNodes[0].ChildNodes[j].Name.Equals(Campo))
                {
                    DadosICMS = nos[i].ChildNodes[0].ChildNodes[j].InnerText;
                }
            }
                return DadosICMS;
        }
        //dados do IPI serão encaminhados com separador "|"
        public string getDadosIPI(XmlNodeList nosICMS, int i, string campo)
        {
            string codDadoIPI = "";
            if (nosICMS.Count != 0&&campo.Equals("cEnq"))
            {
                for (int j = 0; j < nosICMS[i].ChildNodes.Count; j++)
                {
                    if (nosICMS[i].ChildNodes[j].Name.Equals("cEnq"))
                    {
                        codDadoIPI = nosICMS[i].ChildNodes[j].InnerText;
                    }
                }
            }
            else if (nosICMS.Count != 0&&campo.Equals("CST"))
            {
                if (indOper.Equals("0") && CRT.Equals("2"))
                {
                    codDadoIPI = "49";
                }
                else if (indOper.Equals("1") && CRT.Equals("2"))
                {
                    codDadoIPI = "99";
                }
                else
                {
                for (int j = 0; j < nosICMS[i].ChildNodes.Count; j++)
                {
                    if (nosICMS[i].ChildNodes[j].Name.Equals("IPINT"))
                    {
                        for(int k=0;k<nosICMS[i].ChildNodes[j].ChildNodes.Count; k++)
                        {
                            if(nosICMS[i].ChildNodes[j].ChildNodes[k].Name.Equals("CST"))
                            {
                               codDadoIPI = nosICMS[i].ChildNodes[j].ChildNodes[k].InnerText;
                            }

                        }
                    }
                }
                }
            }
            else if (nosICMS.Count != 0 && campo.Equals("vBC"))
            {
                for (int j = 0; j < nosICMS[i].ChildNodes.Count; j++)
                {
                    if (nosICMS[i].ChildNodes[j].Name.Equals("IPINT"))
                    {
                        for (int k = 0; k < nosICMS[i].ChildNodes[j].ChildNodes.Count; k++)
                        {
                            if (nosICMS[i].ChildNodes[j].ChildNodes[k].Name.Equals("vBC"))
                            {
                                codDadoIPI = nosICMS[i].ChildNodes[j].ChildNodes[k].InnerText;
                            }

                        }
                    }
                }
            }
            else if (nosICMS.Count != 0 && campo.Equals("pIPI"))
            {
                for (int j = 0; j < nosICMS[i].ChildNodes.Count; j++)
                {
                    if (nosICMS[i].ChildNodes[j].Name.Equals("IPINT"))
                    {
                        for (int k = 0; k < nosICMS[i].ChildNodes[j].ChildNodes.Count; k++)
                        {
                            if (nosICMS[i].ChildNodes[j].ChildNodes[k].Name.Equals("pIPI"))
                            {
                                codDadoIPI = nosICMS[i].ChildNodes[j].ChildNodes[k].InnerText;
                            }

                        }
                    }

                }
            }
            else if (nosICMS.Count != 0 && campo.Equals("vIPI"))
            {
                for (int j = 0; j < nosICMS[i].ChildNodes.Count; j++)
                {
                    if (nosICMS[i].ChildNodes[j].Name.Equals("IPINT"))
                    {
                        for (int k = 0; k < nosICMS[i].ChildNodes[j].ChildNodes.Count; k++)
                        {
                            if (nosICMS[i].ChildNodes[j].ChildNodes[k].Name.Equals("vIPI"))
                            {
                                codDadoIPI = nosICMS[i].ChildNodes[j].ChildNodes[k].InnerText;
                            }

                        }
                    }
                }
            }
            return codDadoIPI;
        }
        public void setRegAnaliticos()
        {
            for (int i = 0; i < itensNota.Count; i++)
            {
                int cont=0;
                regAnalitico regAnalitico  = new regAnalitico();
                if (i == 0)
                {
                    regAnaliticos.Add(popularRegAnalitico(itensNota, i));
                }
                else if (i > 0)
                {
                    for (int j = 0; j < regAnaliticos.Count; j++)
                    {
                        if (((itemNota)itensNota[i]).cstICMS.Equals(((regAnalitico)regAnaliticos[j]).cstICMS) &&
                            ((itemNota)itensNota[i]).CFOP.Equals(((regAnalitico)regAnaliticos[j]).CFOP) &&
                            ((itemNota)itensNota[i]).aliqICMS.Equals(((regAnalitico)regAnaliticos[j]).aliqICMS))
                        {
                            ((regAnalitico)regAnaliticos[j]).vlOper = ((regAnalitico)regAnaliticos[j]).vlOper + ((Convert.ToDouble(((itemNota)itensNota[i]).vlItem)) - Convert.ToDouble(((itemNota)itensNota[i]).vlDesc));
                            ((regAnalitico)regAnaliticos[j]).vlBCICMS = ((regAnalitico)regAnaliticos[j]).vlBCICMS + Convert.ToDouble(((itemNota)itensNota[i]).vlBCICMS);
                            ((regAnalitico)regAnaliticos[j]).vlICMS = ((regAnalitico)regAnaliticos[j]).vlICMS + Convert.ToDouble(((itemNota)itensNota[i]).vlICMS);
                            ((regAnalitico)regAnaliticos[j]).vlBCICMSST = ((regAnalitico)regAnaliticos[j]).vlBCICMSST + Convert.ToDouble(((itemNota)itensNota[i]).vlBCICMSST);
                            ((regAnalitico)regAnaliticos[j]).vlICMSST = ((regAnalitico)regAnaliticos[j]).vlICMSST + Convert.ToDouble(((itemNota)itensNota[i]).vlICMSST);
                            ((regAnalitico)regAnaliticos[j]).vlIPI = ((regAnalitico)regAnaliticos[j]).vlIPI + Convert.ToDouble(((itemNota)itensNota[i]).vlIPI);
                            cont++;
                        }
                    }
                    if (cont == 0)
                    {
                        regAnaliticos.Add(popularRegAnalitico(itensNota, i));
                    }
                }
                }
        }
        public regAnalitico popularRegAnalitico(ArrayList itensNota, int i)
        {
            regAnalitico regAnal = new regAnalitico();
            regAnal.cstICMS = ((itemNota)itensNota[i]).cstICMS;
            regAnal.CFOP = ((itemNota)itensNota[i]).CFOP;
            regAnal.aliqICMS = ((itemNota)itensNota[i]).aliqICMS;
            regAnal.vlOper = Convert.ToDouble(((itemNota)itensNota[i]).vlItem) - Convert.ToDouble(((itemNota)itensNota[i]).vlDesc);
            regAnal.vlBCICMS = Convert.ToDouble(((itemNota)itensNota[i]).vlBCICMS);
            regAnal.vlICMS = Convert.ToDouble(((itemNota)itensNota[i]).vlICMS);
            regAnal.vlBCICMSST = Convert.ToDouble(((itemNota)itensNota[i]).vlBCICMSST);
            regAnal.vlICMSST = Convert.ToDouble(((itemNota)itensNota[i]).vlICMSST);
            if (((itemNota)itensNota[i]).vlIPI.Equals(""))
            {
                regAnal.vlIPI = 0;
            }
            else
            {
                regAnal.vlIPI = Convert.ToDouble(((itemNota)itensNota[i]).vlIPI);

            } 
            return regAnal;
        }
        public Participante popularparticipante (string nodeXML, XmlDocument doc)
        {
            Participante part = new Participante();
            XmlNodeList nos = doc.GetElementsByTagName(nodeXML);
            for (int i = 0; i < nos[0].ChildNodes.Count; i++)
            {
                if (nos[0].ChildNodes[i].Name.Equals("CNPJ"))
                {
                    part.codPart = nos[0].ChildNodes[i].InnerText;
                    part.cnpj = nos[0].ChildNodes[i].InnerText;
                }
                else if (nos[0].ChildNodes[i].Name.Equals("CPF"))
                {
                    part.codPart = nos[0].ChildNodes[i].InnerText;
                    part.CPF = nos[0].ChildNodes[i].InnerText;
                }
                else if (nos[0].ChildNodes[i].Name.Equals("xNome"))
                {
                    
                    part.razaosocial = nos[0].ChildNodes[i].InnerText;
                }
                else if (nos[0].ChildNodes[i].Name.Equals("IE"))
                {
                    
                    part.IE = nos[0].ChildNodes[i].InnerText;
                }
                else if (nos[0].ChildNodes[i].Name.Equals("enderEmit") || nos[0].ChildNodes[i].Name.Equals("enderDest"))
                {
                    for (int j = 0; j < nos[0].ChildNodes[i].ChildNodes.Count; j++)
                    {
                        if (nos[0].ChildNodes[i].ChildNodes[j].Name.Equals("xLgr"))
                        {
                            part.logradouro = nos[0].ChildNodes[i].ChildNodes[j].InnerText;
                        }
                        else if (nos[0].ChildNodes[i].ChildNodes[j].Name.Equals("nro"))
                        {
                            part.numero = nos[0].ChildNodes[i].ChildNodes[j].InnerText;
                        }
                        else if (nos[0].ChildNodes[i].ChildNodes[j].Name.Equals("xBairro"))
                        {
                            part.bairro = nos[0].ChildNodes[i].ChildNodes[j].InnerText;
                        }
                        else if (nos[0].ChildNodes[i].ChildNodes[j].Name.Equals("cMun"))
                        {
                            part.codMun = nos[0].ChildNodes[i].ChildNodes[j].InnerText;
                        }
                        else if (nos[0].ChildNodes[i].ChildNodes[j].Name.Equals("xCpl"))
                        {
                            part.complemento= nos[0].ChildNodes[i].ChildNodes[j].InnerText;
                            
                        }
                        else if (nos[0].ChildNodes[i].ChildNodes[j].Name.Equals("xPais"))
                        {
                            if (nos[0].ChildNodes[i].ChildNodes[j].InnerText.Equals("BRASIL"))
                                part.codPais = "1058";

                        }
                        else if (nos[0].ChildNodes[i].ChildNodes[j].Name.Equals("cPais"))
                        {
                            part.codPais = nos[0].ChildNodes[i].ChildNodes[j].InnerText;
                         }
                    }
                }
                
            }
            return part;
        }
    }
}
