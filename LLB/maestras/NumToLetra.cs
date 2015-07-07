using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Brainsbits.LLB.maestras
{
    public class NumToLetra
    {
        private String[] UNIDADES = { "", "un ", "dos ", "tres ", "cuatro ", "cinco ", "seis ", "siete ", "ocho ", "nueve " };
        private String[] DECENAS = { "diez ", "once ", "doce ", "trece ", "catorce ", "quince ", "dieciseis ", "diecisiete ", "dieciocho ", "diecinueve", "veinte ", "treinta ", "cuarenta ", "cincuenta ", "sesenta ", "setenta ", "ochenta ", "noventa " };
        private String[] CENTENAS = { "", "ciento ", "doscientos ", "trecientos ", "cuatrocientos ", "quinientos ", "seiscientos ", "setecientos ", "ochocientos ", "novecientos " };

        private Regex r;

        public String Convertir(String numero, bool mayusculas)
        {
            String literal = "";
            String parte_decimal;
            numero = numero.Replace(".", ",");

            if (numero.IndexOf(",") == -1)
            {
                numero = numero + ",00";
            }

            numero = Math.Round(Convert.ToDecimal(numero), 2).ToString();

            r = new Regex(@"\d{1,9},\d{1,2}");

            MatchCollection mc = r.Matches(numero);

            if (mc.Count > 0)
            {
                String[] Num = numero.Split(',');

                if (int.Parse(Num[1]) == 0)
                {
                    parte_decimal = "";
                }
                else
                {
                    if (int.Parse(Num[1]) > 999)
                    {
                        parte_decimal = getMiles(Num[0]);
                    }
                    else if (int.Parse(Num[1]) > 99)
                    {
                        parte_decimal = getCentenas(Num[0]);
                    }
                    else if (int.Parse(Num[1]) > 9)
                    {
                        parte_decimal = getDecenas(Num[1]);
                    }
                    else
                    {
                        parte_decimal = getUnidades(Num[1]);
                    }
                }

                if (parte_decimal != "")
                {
                    parte_decimal = "con " + parte_decimal + " centavos";
                }

                if (int.Parse(Num[0]) == 0)
                {
                    literal = "cero ";
                }
                else if (int.Parse(Num[0]) > 999999)
                {
                    literal = getMillones(Num[0]);
                }
                else if (int.Parse(Num[0]) > 999)
                {
                    literal = getMiles(Num[0]);
                }
                else if (int.Parse(Num[0]) > 99)
                {
                    literal = getCentenas(Num[0]);
                }
                else if (int.Parse(Num[0]) > 9)
                {
                    literal = getDecenas(Num[0]);
                }
                else
                {
                    literal = getUnidades(Num[0]);
                }

                if (mayusculas)
                {
                    return (literal + "pesos " + parte_decimal).ToUpper();
                }
                else
                {
                    return (literal + "pesos " + parte_decimal);
                }
            }
            else
            {
                return literal = null;
            }
        }


        private String getUnidades(String numero)
        {   
            String num = numero.Substring(numero.Length - 1);
            return UNIDADES[int.Parse(num)];
        }

        private String getDecenas(String num)
        {
            int n = int.Parse(num);
            if (n < 10)
            {
                return getUnidades(num);
            }
            else if (n > 19)
            {
                String u = getUnidades(num);
                if (u.Equals(""))
                {
                    return DECENAS[int.Parse(num.Substring(0, 1)) + 8];
                }
                else
                {
                    return DECENAS[int.Parse(num.Substring(0, 1)) + 8] + "y " + u;
                }
            }
            else
            {
                return DECENAS[n - 10];
            }
        }

        private String getCentenas(String num)
        {
            if (int.Parse(num) > 99)
            {
                if (int.Parse(num) == 100)
                {
                    return " cien ";
                }
                else
                {
                    return CENTENAS[int.Parse(num.Substring(0, 1))] + getDecenas(num.Substring(1));
                }
            }
            else
            {
                return getDecenas(int.Parse(num) + "");
            }
        }

        private String getMiles(String numero)
        {
            String c = numero.Substring(numero.Length - 3);
            String m = numero.Substring(0, numero.Length - 3);
            String n = "";
            if (int.Parse(m) > 0)
            {
                n = getCentenas(m);
                return n + "mil " + getCentenas(c);
            }
            else
            {
                return "" + getCentenas(c);
            }

        }

        private String getMillones(String numero)
        {
            String miles = numero.Substring(numero.Length - 6);
            String millon = numero.Substring(0, numero.Length - 6);
            String n = "";
            if (millon.Length > 1)
            {
                n = getCentenas(millon) + "millones ";
            }
            else
            {
                n = getUnidades(millon) + "millon ";
            }
            return n + getMiles(miles);
        }
    }
}
