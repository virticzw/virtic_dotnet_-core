using System;
using ServiceReference1;

namespace virtic_dotnet__core
{
    class virtic_periode
    {
        static void Main(string[] args)
        {
            virtic_periode program;
            program = new virtic_periode();
            program.fetchPeriode();

        }
        private void fetchPeriode()
        {

            
            wsPeriodeObjClient VirServerObj = null;
            periodeObjClient periodeObjClient = null;
            wsPeriodeID VirServerUUID = null;
            periodeID periodeUUID = null;
            Console.WriteLine("Start!");
            try
            {
                VirServerObj = new wsPeriodeObjClient();
                // Hier tragen sie die vom virtic Kundendienst erhaltenen AnmeldeInformationen ein.

                VirServerUUID = VirServerObj.Connect_wsPeriode("UserID", "Kennwort", "Domäne");


                Console.WriteLine("Connected:" + VirServerUUID.UUID.ToString());
                string result;
                periodeUUID = VirServerObj.CreatePO_periode(VirServerUUID, out result);
                periodeV100OutRow[] periodeV100OutRows;
                periodeObjClient = new periodeObjClient();
                periodeObjClient.fetchV100(periodeUUID, null, "", "", out periodeV100OutRows, out result);
                Console.WriteLine("Records:" + periodeV100OutRows.Length);
                foreach (periodeV100OutRow row in periodeV100OutRows) {
                    Console.WriteLine(row.perJahr.ToString() + "/" + row.perMonat.ToString() + ", eröffnet:" + row.perEroeffnet + ", geschl.:" + row.perGeschlossen);
                }
                Console.WriteLine("Fertig:");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Fehler:" + ex.Message);
            }
            finally
            {
                if (periodeObjClient != null && periodeUUID != null)
                    periodeObjClient.Release_periode(periodeUUID);
                if (VirServerObj != null && VirServerUUID != null)
                    VirServerObj.Release_wsPeriode(VirServerUUID);

            }

        }
    }
}

