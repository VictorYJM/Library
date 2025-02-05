﻿using System;
using System.IO;

namespace apCaminhosEmMarte
{
    public class Cidade : IRegistro<Cidade>, IComparable<Cidade>
    {
        // mapeamento da linha de dados do arquivo de cidades
        const int
          tamNome = 15,
          tamX = 7,
          tamY = 7,
          inicioNome = 0,
          inicioX = inicioNome + tamNome,
          inicioY = inicioX + tamX;

        string nomeCidade;
        double x, y;

        public Cidade()
        {

        }

        public Cidade(string nome, double x, double y)
        {
            NomeCidade = nome;
            X = x;
            Y = y;
        }

        public string Chave => NomeCidade;

        public string NomeCidade
        {
            get => nomeCidade;
            set
            {
                nomeCidade = value.PadRight(tamNome, ' ').Substring(0, tamNome);
            }
        }

        public double X
        {
            get => x;
            set
            {
                if (value < 0 || value > 1)
                    throw new Exception("X fora do intervalo de 0 a 1");

                x = value;
            }
        }

        public double Y
        {
            get => y;
            set
            {
                if (value < 0 || value > 1)
                    throw new Exception("Y fora do intervalo de 0 a 1");

                y = value;
            }
        }

        public void GravarDados(StreamWriter arquivo)
        {
            if (arquivo != null)  // está aberto para escrita
                arquivo.WriteLine($"{NomeCidade}{X,7:F5}{Y,7:F5}");
        }

        public void LerRegistro(StreamReader arquivo)
        {
            if (arquivo != null)  // arquivo foi aberto
                if (!arquivo.EndOfStream) // se não acabou de ler
                {
                    string linhaLida = arquivo.ReadLine();

                    // separamos cada campo a partir da linha lida
                    NomeCidade = linhaLida.Substring(inicioNome, tamNome);
                    string strX = linhaLida.Substring(inicioX, tamX);
                    X = double.Parse(strX);
                    Y = double.Parse(linhaLida.Substring(inicioY, tamY));
                }
        }

        // toString modificado para sempre retornar uma cadeia formatada
        public override string ToString()
        {
            string strX = x.ToString("0.00000");
            string strY = y.ToString("0.00000");
            return ($"{NomeCidade}{strX}{strY}");
        }

        public int CompareTo(Cidade outro)
        {
            return nomeCidade.CompareTo(outro.NomeCidade);
        }
    }
}
