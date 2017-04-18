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
    public partial class frmgrdCLIE : Form
    {
        public frmgrdCLIE()
        {
            InitializeComponent();
            this.btnSupprimer.Enabled = false;
            afficheClient();
        }
        /// <summary>
        /// fermer la grille
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFermergrdClient_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        /// <summary>
        ///   instancier un form de saisie de Client puis réafficher la datagridview 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAjouterClient_Click(object sender, EventArgs e)
        {
            frmNewClient frmc = new frmNewClient();
           if(frmc.ShowDialog()==DialogResult.OK)
            {
                afficheClient();
            }
        }
        /// <summary>
        ///  rétablit la source de données de la dataGridView et rafraîchit son affichage 
        /// </summary>
        private void afficheClient()
        {
            // DataTable : pour recopier les Client 
            // (stockés en collection) 
            // à relier au DataGridView pour personnaliser son affichage

            DataTable dt = new DataTable();
            DataRow dr;
            //cree une colone qui souvegarde l index 
           // dt.Columns.Add(new DataColumn("indexClient",typeof(Int32 )));
            // ajout à la datatable de 4 colonnes personnalisées
            dt.Columns.Add(new DataColumn("Numero de Client"));
            dt.Columns.Add(new DataColumn("Raison Sociale"));
            dt.Columns.Add(new DataColumn("Ville du Client"));
            dt.Columns.Add(new DataColumn("Code Postale Du Client"));
            // boucle remplissage de la DataTable à partir de la collection
            // for (int i = 0; i < Donnees.ArrayClient.Count; i++)
            //  { 

            foreach (Clients unClientEF in Donnees.DB.Clients.ToList())
            { 
            dr = dt.NewRow();

               // dr[0] = i;
                dr[0] = unClientEF.Numero_Client;
                dr[1] = unClientEF.Raison_Social;
                dr[2] = unClientEF.Ville;
                dr[3] = unClientEF.Code_postal;
                dt.Rows.Add(dr);

            }

            // déterminer l'origine des données à afficher en DataGridView
            this.grdClient.DataSource = dt.DefaultView;

            //rendre invisible ma premiere colomn
            grdClient.Columns[0].Visible = true;
            

        }      
        /// <summary>
        /// supprime un client de la grille
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            if (grdClient.RowCount != 0 )
            {
                DialogResult rep;
                rep = MessageBox.Show("Voulez vous vraiment supprimer le Contact", "Supprimer", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rep == DialogResult.Yes)
                {
                    Int32 idClient;
                    idClient = (Convert.ToInt32(grdClient.CurrentRow.Cells[0].Value));
                    Clients  leClientEF = Donnees.DB.Clients.Find(idClient);
                    Donnees.DB.Clients.Remove(leClientEF);
                    Donnees.DB.SaveChanges();
                   // Donnees.ArrayClient.RemoveAt(Convert.ToInt32(grdClient[0, grdClient.CurrentRow.Index].Value));       // Renaud the great  idea 
                    this.afficheClient();
                    this.btnSupprimer.Enabled = false;
                }
                
            }
        }
        /// <summary>
        /// rechercher un client dans ma grille, fait recherche sur tt les colomns
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRechercherClient_Click(object sender, EventArgs e)
        {
            ((DataView)(this.grdClient.DataSource)).RowFilter = "[Numero de Client] like '%" + this.txtRechercherClient.Text +
                  "%' or [Raison Sociale] like '%" + this.txtRechercherClient.Text +
                  "%' or [Ville du Client] like '%" + this.txtRechercherClient.Text +
                  "%' or [Code Postale Du Client] like '%" + this.txtRechercherClient.Text + "%' ";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       

        private void grdClient_DoubleClick(object sender, EventArgs e)
        {
            if (grdClient.RowCount != 0)
            {
                this.btnSupprimer.Enabled = true;
            }

            if (grdClient.CurrentRow != null)
            {
                Int32 iClient;
                Boolean isIClientValid = Int32.TryParse(grdClient.CurrentRow.Cells[0].Value.ToString(), out iClient);
                if (isIClientValid)
                {
                    Clients leClient = Donnees.DB.Clients.Find(iClient);

                    frmCLIE frmclient = new frmCLIE(leClient);

                    if (frmclient.ShowDialog() == DialogResult.OK)
                    {
                        this.afficheClient();
                    }
                }
                
            }
        }
    }
}
