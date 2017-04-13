using System;
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
    public partial class frmCLIE : Form
    {
        //j instancie un client
        private Clients LeClient;
       
        public frmCLIE(Clients unClient)
        {
            this.LeClient = unClient;
            InitializeComponent();}

/// <summary>
/// je charge mes combobox ainsi j aaffiche la fiche client
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
        private void frmCLIE_Load(object sender, EventArgs e)
        {
            cbxNatureClient.Items.AddRange(new String[] { "principale", "secondaire", "ancienne" });
            cbxTypeClient.Items.AddRange(new String[] { "public", "privee" });
            this.afficheClient(LeClient);
        }
        /// <summary>
        /// methode  affiche en textbox les données d'un client
        /// </summary>
        /// <param name="unClient"></param>
        private void afficheClient(Clients unClient)

        {

            this.txtNumeroDeClient.Text = unClient.Numero_Client.ToString();
            this.txtRaisonSocialeDuClient.Text = unClient.Raison_Social;
            this.txtDomaineDactivite.Text = unClient.Domaine;
            this.txtAdresseDuClient.Text = unClient.Adresse;
            this.txtVille.Text = unClient.Ville;
            this.txtCodePostale.Text = unClient.Code_postal.ToString();
            this.txtTelephone.Text = unClient.Telephone.ToString();
            this.txtChiffreDaffaire.Text = unClient.Chiffre_d_affaire.ToString();
            this.txtEffectifs.Text = unClient.Effectif.ToString();
            this.cbxNatureClient.Text = unClient.Nature;
            this.cbxTypeClient.Text = unClient.Type;
            //this.txtCommentClient.Text = unClient.;

           // this.txtNumeroDeClient.Enabled = false;
        }
/// <summary>
/// fermer la fenetre 
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
        private void btnFermerClient_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        /// <summary>
        /// quand je click sur contact j ouvre la grille contacts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnContactClient_Click(object sender, EventArgs e)
        {
            frmgrdCONTA frmcontact = new frmgrdCONTA(LeClient);
            DialogResult rep = frmcontact.ShowDialog();

        }
/// <summary>
/// modifier un client
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
        private void btnModifierClient_Click(object sender, EventArgs e)
        {
            int a = Int32.Parse(txtNumeroDeClient.Text);
            
        
            if (this.controle())
            {//parcourir la liste des clients pour chercher le client concerné apres modifier  ses attribus
                for (int i = 0; i < Donnees.DB.Clients.Count(); i++)
                {
                    if (Int32.Parse(txtNumeroDeClient.Text.Trim()) == Donnees.DB.Clients.Find(i).Numero_Client)
                    {
                        Clients client = Donnees.DB.Clients.Find(i);
                        client.Numero_Client = a;
                        client.Raison_Social = txtRaisonSocialeDuClient.Text;
                        client.Domaine = txtDomaineDactivite.Text;
                        client.Adresse = txtAdresseDuClient.Text;
                        client.Ville = txtVille.Text;
                        client.Telephone = txtTelephone.Text;
                        client.Chiffre_d_affaire = decimal.Parse(txtChiffreDaffaire.Text.Trim());
                        client.Code_postal = txtCodePostale.Text;
                        client.Effectif = txtEffectifs.Text;
                        client.Nature = cbxNatureClient.Text;
                        client.Type = cbxTypeClient.Text;
                    //    client.comment = txtCommentClient.Text;
                    }
                }               
                this.DialogResult = DialogResult.Cancel;
            }
        }
        /// <summary>
        /// methode pour gerer les exceptions 
        /// </summary>
        /// <returns></returns>
        public Boolean controle()
        {
            Boolean code = true;


            if (this.txtRaisonSocialeDuClient.Text == "")
            {
                code = false;
                MessageBox.Show("veullez indiquer une raison sociale", "Erreur", MessageBoxButtons.OK);
            }

            if (!(Outils.EstEntier(this.txtNumeroDeClient.Text)))
            {
                code = false;
                MessageBox.Show("Le numero de client saisi n'est pas un entier valide", "Erreur", MessageBoxButtons.OK);
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
            if (!Double.TryParse(txtChiffreDaffaire.Text, out ca))
            {
                code = false;
                MessageBox.Show("CA must be a double", "Erreur", MessageBoxButtons.OK);
            }
            return code;
        }
    }
}

