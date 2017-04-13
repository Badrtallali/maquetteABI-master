﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace maquetteABI
{
    public partial class frmNewClient : Form
    {
        public frmNewClient()
        {
            InitializeComponent();
        }
        /// <summary>
        /// bouton ignorer ferme la fenetre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnIgnorerClient_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        /// <summary>
        /// je charge mes combobox 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmNewClient_Load(object sender, EventArgs e)
        {
            cbxNatureClient.Items.AddRange(new String[] { "principale", "secondaire", "ancienne" });
            cbxTypeClient.Items.AddRange(new String[] { "public", "privee" });
        }
        /// <summary>
        ///si tout control est ok  on instancie un nouveau client et on incremente le nombre des clients dans la liste des clients
         /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnValiderClient_Click(object sender, EventArgs e)
        {
            if (this.controle())
            {
                if (this.instancie())
                {
                    Client.Nclient += 1;

                    this.DialogResult = DialogResult.OK;
                }
            }
        }
        /// <summary>
        /// methode pour gerer les exceptions 
        /// </summary>
        /// <returns></returns>

        public Boolean controle()
        {
            Boolean code = true;
            if (!(Outils.EstEntier(this.txtNumeroDeClient.Text)))
            {
                code = false;
                MessageBox.Show("Le numero de client saisi n'est pas un entier valide", "Erreur", MessageBoxButtons.OK);
            }
            for (int i = 0; i < Donnees.DB.Clients.Count();i++ )
                {
                    if (!(Int32.Parse(txtNumeroDeClient.Text.Trim()) == Donnees.DB.Clients.Find(i).Numero_Client))
                    {
                    code = true;
                }
                       
                    else
                    {
                        code=false;
                        MessageBox.Show("le numero de Client est deja atibuer", "Erreur", MessageBoxButtons.OK);
                    }

                }


            if (this.txtRaisonSocialeDuClient.Text =="")
            {
                code = false;
                MessageBox.Show("veullez indiquer une raison sociale", "Erreur", MessageBoxButtons.OK);
            }

            if (!(Outils.EstEntier(this.txtCodePostale.Text)) || this.txtCodePostale.Text.Length != 5)
            {
                code = false;
                MessageBox.Show("Le code postal saisi n'est pas correct", "Erreur", MessageBoxButtons.OK);
            }

            if (!(Outils.EstEntier(this.txtTelephone.Text)))
            {
                code = false;
                MessageBox.Show("le numero de telephone saisi n'est pas un entier valide", "Erreur", MessageBoxButtons.OK);
            }

            if (!(Outils.EstEntier(this.txtEffectifs.Text)))
            {
                code = false;
                MessageBox.Show("l'Effectifs saisi n'est pas un entier valide", "Erreur", MessageBoxButtons.OK);
            }
            Double ca;
            if(!Double.TryParse(txtChiffreDaffaire.Text, out ca)){
                code = false;
                MessageBox.Show("CA must be a double", "Erreur", MessageBoxButtons.OK);
            }
            return code;
        }
        /// <summary>
        /// instancier un client 
        /// </summary>
        /// <returns></returns>
        private Boolean instancie()
        {
            Client nouveauClient = new Client();
            try
            {
                nouveauClient.NumClient = Int32.Parse(txtNumeroDeClient.Text.Trim());
                nouveauClient.RaisoClient = txtRaisonSocialeDuClient.Text;
                nouveauClient.DomaineClient = txtDomaineDactivite.Text;
                nouveauClient.AdresClient = txtAdresseDuClient.Text;
                nouveauClient.VilleClient = txtVille.Text;
                nouveauClient.TelephoneClient = txtTelephone.Text;
                nouveauClient.ChiffClient = decimal.Parse(txtChiffreDaffaire.Text.Trim());
                nouveauClient.CodePostalClient = txtCodePostale.Text;
                nouveauClient.EffeClient = txtEffectifs.Text;
                nouveauClient.NatClient = cbxNatureClient.Text;
                nouveauClient.TypClient = cbxTypeClient.Text;
                Clients nouveauClientEF = new Clients();
                nouveauClientEF.Numero_Client = nouveauClient.NumClient;
                nouveauClientEF.Raison_Social = nouveauClient.RaisoClient;
                nouveauClientEF.Domaine = nouveauClient.DomaineClient;
                nouveauClientEF.Adresse = nouveauClient.AdresClient;
                nouveauClientEF.Ville = nouveauClient.VilleClient;
                nouveauClientEF.Telephone = nouveauClient.TelephoneClient;
                nouveauClientEF.Chiffre_d_affaire = nouveauClient.ChiffClient;
                nouveauClientEF.Code_postal = nouveauClient.CodePostalClient;
                nouveauClientEF.Effectif = nouveauClient.EffeClient;
                nouveauClientEF.Nature = nouveauClient.NatClient;
                nouveauClientEF.Type = nouveauClient.TypClient;
                Donnees.DB.Clients.Add(nouveauClientEF);
                Donnees.DB.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                nouveauClient = null;
                MessageBox.Show("Erreur : \n" + ex.Message, "Ajout de Client");
                    return false;
            }


        }
    }
}
