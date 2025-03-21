using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BddtournoiContext; 

namespace DllTournois
{   
    /// <summary>
    /// Classe permettant d'exploiter la  BDD Bddtournoi.
    /// </summary>
    public class Bddtournoi
    {

        //Objet de la classe de la BDD générée par LinQ
        private static BddtournoiDataContext DB;

        /// <summary>
        /// essaye de se connecter et d'ouvrir la connexion à la BDD en utilisant les valeurs passés en paramètres.
        /// </summary>
        public static void Connect(string host, string user, string password, string dataBase, string port)
        {
            DB = new BddtournoiDataContext("User Id=" + user + ";Password=" + password + ";Host=" + host +
                                                ";Port=" + port + ";Database=" + dataBase + ";Persist Security Info=True");
            DB.Connection.Open();
        }

        /// <summary>
        /// permet de tester si la connexion à la BDD est ouverte
        /// </summary>
        public static bool TestConnection()
        {
            return DB.Connection.State == System.Data.ConnectionState.Open; 
        }


        public static List<Sport> GetSports()
        {
            return DB.Sports.ToList();
        }

        public static List<Tournoi> GetTournois(int sportId)
        {
            return DB.Tournois.Where(t => t.Sport == sportId).ToList();
        }

        public static List<Tournoi> GetTournois()
        {
            return DB.Tournois.ToList();
        }

        public static List<Participant> GetParticipants(int tournoiID)
        {
            return DB.Participants.Where(p => p.Tournoi == tournoiID).ToList();
        }

        public static List<Participant> GetParticipants()
        {
            return DB.Participants.ToList(); 
        }

        public static List<Participant> GetParticipants(string nom)
        {
            return DB.Participants.Where(p => p.Nom.ToUpper().Contains(nom.ToUpper())).ToList();
        }


        public static bool AccountExists(string login)
        {
            int nb = DB.Gestionnairesapplis.Count(g => g.Login == login);
            return nb != 0; 
        }

        public static bool CheckPassword(string login, string mdp) 
        {
            int nb = DB.Gestionnairesapplis.Where(g => g.Login == login).Count(g => g.MotDpass == mdp);
            return nb != 0; 
        }

        public static void ajouterTournoi(string intitule, System.DateTime date, Sport sport)
        {
            int nb = DB.Tournois.Where(g => g.Intitule.ToUpper() == intitule.ToUpper() && g.Sport == sport.IdSport && g.DateTournoi == date).Count();
            if (nb > 0)
                throw new Exception("Un tournoi pour ce sport, avec le même intitulé et la même date existe déjà.");
            else
            {
                DB.Tournois.InsertOnSubmit(new Tournoi(intitule, date, sport));
                DB.SubmitChanges();
            }
        }

        public static void supprimerTournoi(Tournoi tournoi)
        {
            List<Participant> participants = DB.Participants.Where(p => p.Tournoi == tournoi.IdTournoi).ToList();  
            DB.Participants.DeleteAllOnSubmit(participants);
            DB.Tournois.DeleteOnSubmit(tournoi);
            DB.SubmitChanges();  
        }

        public static void modifierTournoi(Tournoi tournoi)
        {
            Tournoi t = DB.Tournois.Where(tou => tou.IdTournoi == tournoi.IdTournoi).Single() ;
            t.Intitule = tournoi.Intitule;
            t.DateTournoi = tournoi.DateTournoi;
            t.Sport = tournoi.Sport;
            DB.SubmitChanges(); 
        }

        public static void ajouterSport(string intitule)
        {
            int nb = DB.Sports.Where(s => s.Intitule.ToUpper() == intitule.ToUpper()).Count();
            if (nb > 0)
                throw new Exception("Un Sport avec ce nom existe déjà");
            else
            {
                DB.Sports.InsertOnSubmit(new Sport(intitule));
                DB.SubmitChanges();
            }
        }

        public static void supprimerSport(Sport sport)
        {
            List<Tournoi> tournois = DB.Tournois.Where(t => t.Sport == sport.IdSport).ToList(); 
            DB.Tournois.DeleteAllOnSubmit(tournois);
            DB.Sports.DeleteOnSubmit(sport);
            DB.SubmitChanges();
        }

        public static void modifierSport(Sport sport)
        {
            Sport s = DB.Sports.Where(spo => spo.IdSport == sport.IdSport).Single();
            s.Intitule = sport.Intitule; 
            DB.SubmitChanges();
        }

        public static void ajouterParticipants(string nom, string prenom, string sex, DateTime dateN, Tournoi tournoi, byte[] photo)
        {
            int nb = DB.Participants.Where(s => s.Nom.ToUpper() == nom.ToUpper()  &&  s.Prenom.ToUpper() == prenom.ToUpper() && 
                                               s.Sexe.ToUpper() == sex.ToUpper() && s.DateNaissance == dateN &&  s.Tournoi == tournoi.IdTournoi ).Count();
            if (nb > 0)
                throw new Exception("Ce participant existe déjà pour ce tounoi");
            else
            {
                DB.Participants.InsertOnSubmit(new Participant(nom, prenom, dateN, sex, photo, tournoi));
                DB.SubmitChanges(); 
            }
        }

        public static void supprimerParticipant(Participant participant)
        {
            DB.Participants.DeleteOnSubmit(participant);
            DB.SubmitChanges();
        }

        public static void modifierParticipant(Participant participant)
        {
            Participant p = DB.Participants.Where(par => par.Id == participant.Id).Single();
            p.Prenom = participant.Nom;
            p.Nom = participant.Nom;
            p.DateNaissance = participant.DateNaissance;
            p.Sexe = participant.Sexe;
            p.Photo = participant.Photo;
            p.Tournoi = participant.Tournoi; 
            DB.SubmitChanges();
        }



    }
}
