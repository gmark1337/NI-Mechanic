namespace Mechanic
{

    //    Munka
    // • Munka azonosító(EF által automatikusan generált)

    // • Ügyfélszám

    // • Gépjármű rendszáma– Validációs kikötés: Megfelelő rendszám formátummal legyen ellátva:

    //  XXX-YYY(X: nagy betűk, Y: számok)

    // • Gépjármű gyártási éve– Validációs kikötés: A gyártási év ne lehessen 1900-nál kisebb

    // • Munka kategóriája– Itt megengedett értékek: Karosszéria, motor, futómű, fékberendezés

    // • Gépjármű hibáinak rövid leírása

    // • Ahiba súlyossága– Amegengedett érték 1-10 intervallumba eshet

    // • Munka állapota– Megengedett értékek: Felvett Munka-> Elvégzés alatt-> Befejezett–
    //  Az értékeket csak a nyílnak megfelelő irányba lehessen változtatni
    public class Job
    {
        //TODO: Job(jobId, id, licensePlate, ManufacturingYear, Category, Description, Severity,Status -> VALIDATION!!

        public string jobId { get; set; }

        public string customerId { get; set; }

        public string licensePlate { get; set; }

        public int manufactingYear { get; set; }

        public string description { get; set; }

        public int severity { get; set; }

        public string status { get; set; }


    }
}
