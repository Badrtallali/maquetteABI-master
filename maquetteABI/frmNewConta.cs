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
    public partial class frmNewConta : Form
    {
        private Clients client;
        public frmNewConta(Clients client)
        {
            InitializeComponent();
            this.client = client;
        }
        /// <summary>
        /// bouton ignorer ferme la fenetre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnIgnorerContact_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }       
        /// <summary>
        /// si tout control est ok  on instancie un nouveau contact et on incremente le nombre des contact dans la liste des contact client
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnValiderContact_Click(object sender, EventArgs e)
        {
            if (this.controle())
            {
                if (this.instancie())
                {
                    Contact.Ncontact+= 1;

                    this.DialogResult = DialogResult.OK;
                }
            }
        }
        /// <summary>
        /// instancier un client 
        /// </summary>
        /// <returns></returns>
        private Boolean instancie()
        {
            Contacts nouveauContact = new Contacts();
            try
            {
                nouveauContact.Numero = Int32.Parse(txtNumContact.Text.Trim());
                nouveauContact.Nom = txtNomContact.Text.Trim().ToUpper();
                nouveauContact.Prenom = txtPrenomContact.Text.Trim().ToUpper();
                nouveauContact.Fonction = txtFonctionContact.Text;
                nouveauContact.Telephone = txtTelephoneContact.Text;
                nouveauContact.Mail = txtAdresseMailContact.Text;

                client.Contacts.Add(nouveauContact);
                return true;
            }
            catch (Exception ex)
            {
                nouveauContact = null;
                MessageBox.Show("Erreur : \n" + ex.Message, "Ajout de Contact");
                return false;
            }

        }
        /// <summary>
        /// methode pour gerer les exceptions 
        /// </summary>
        /// <returns></returns>
        public Boolean controle()
        {
            Boolean code = true;
            if (!(Outils.EstEntier(this.txtNumContact.Text)))
            {
                code = false;
                MessageBox.Show("Le numero de contact saisi n'est pas un entier valide", "Erreur", MessageBoxButtons.OK);
            }
            for (int i = 0; i < client.Contacts.Count; i++)
            {
                if (!(Int32.Parse(txtNumContact.Text.Trim()) == client.Contacts.ElementAt(i).Numero))
                    code = true;
                else
                {
                    code = false;
                    MessageBox.Show("le numero de Contact est deja atibuer", "Erreur", MessageBoxButtons.OK);
                }
            }
            if (!(Outils.EstEntier(this.txtTelephoneContact.Text)))
            {
                code = false;
                MessageBox.Show("Le numero de telephone  saisi n'est pas correct", "Erreur", MessageBoxButtons.OK);
            }


            if (this.txtNomContact.Text == "")
            {
                code = false;
                MessageBox.Show("veullez indiquer un nom au contact", "Erreur", MessageBoxButtons.OK);
            }
            
            return code;
        }
    }
}
