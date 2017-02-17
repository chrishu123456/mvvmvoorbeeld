using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace mvvmvoorbeeld.ViewModel
{
    public class TekstMetOpmaakVM : ViewModelBase
    {
        private mvvmvoorbeeld.Model.TekstMetOpmaak opgemaakteTekst;

        public string Inhoud
        {
            get { return opgemaakteTekst.inhoud; }
            set
            {
                opgemaakteTekst.inhoud = value;
                RaisePropertyChanged("Inhoud");
            }
        }

        public Boolean Vet
        {
            get { return opgemaakteTekst.Vet; }
            set
            {
                opgemaakteTekst.Vet = value;
                RaisePropertyChanged("Vet");
            }
        }

        public Boolean Schuin
        {
            get { return opgemaakteTekst.Schuin; }
            set
            {
                opgemaakteTekst.Schuin = value;
                RaisePropertyChanged("Schuin");
            }
        }

        public RelayCommand NieuwCommand
        {
            get { return new RelayCommand(NieuweTekstBox); }
        }




        private RelayCommand opslaanCommandValue;

        public RelayCommand OpslaanCommand
        {
            get { return new RelayCommand(OpslaanBestand); }
        }

        private RelayCommand openCommandValue;

        public RelayCommand OpenCommand
        {
            get { return new RelayCommand(OpenenBestand); }

        }

        private RelayCommand afluitenCommandValue;

        public RelayCommand AfsluitenCommand
        {
            get { return new RelayCommand(AfsluitenTextBox); }
        }


        private void NieuweTekstBox()
        {
            Inhoud = string.Empty;
            Vet = false;
            Schuin = false;
        }

        private void OpslaanBestand()
        {
            try
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.FileName = "TekstBox";
                dlg.Filter = "tekst bestand|*.txt";
                dlg.DefaultExt = ".txt";

                if (dlg.ShowDialog()== true)
                {
                    using (StreamWriter bestand = new StreamWriter(dlg.FileName))
                    {
                        bestand.WriteLine(Inhoud);
                        bestand.WriteLine(Vet.ToString());
                        bestand.WriteLine(Schuin.ToString());
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Opslaan mislukt : " + ex.Message);
            }

        }

        private void OpenenBestand()
        {
            try
            {
                OpenFileDialog dlg =new OpenFileDialog();
                dlg.FileName = "TextBox";
                dlg.Filter = "tekst bestand|*.txt";
                dlg.DefaultExt = ".txt";

                if (dlg.ShowDialog() == true)
                {
                    using (StreamReader bestand = new StreamReader(dlg.FileName))
                    {
                        Inhoud = bestand.ReadLine();
                        Vet = Convert.ToBoolean(bestand.ReadLine());
                        Schuin = Convert.ToBoolean(bestand.ReadLine());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("openen mislukt" + ex.Message);
            }
        }

        private void AfsluitenTextBox()
        {
            Application.Current.MainWindow.Close();
        }

        public TekstMetOpmaakVM(Model.TekstMetOpmaak opgemaaktetekst)
        {
            this.opgemaakteTekst = opgemaaktetekst;
        }


    }
}
