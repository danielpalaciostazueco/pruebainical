using Newtonsoft.Json;
namespace bankApp
{
    public class bankAccounts
    {
        MenuBank menuAccount = new MenuBank();
        bankTransactions transAccount = new bankTransactions();
        public string clientName { get; set; }
        public string ClientDirection { get; set; }
        public string ClientPhoneNumber { get; set; }
        public string ClientCurrentMoney { get; set; }

        //Constructor
        /*
        public bankAccounts(string owner , string id, float amount){
            this.amountAccount = amount;
            this.idAccount = id;
            this.ownerAccount = owner;
        }*/
        //Métodos get

        public string getClientName()
        {
            return this.clientName;
        }

        public string getClientDirection()
        {
            return this.ClientDirection;
        }
        public string getClientPhone()
        {
            return this.ClientPhoneNumber;
        }

        public string getClientMoney()
        {
            return this.ClientCurrentMoney;
        }

        public void createAccount()
        {
            Console.WriteLine("Vamos a proceder a rellenar los datos para crear la cuenta");
            Console.WriteLine("-----------------------------------------------------------");

            Console.WriteLine("Introduce el nombre y apellidos");
            this.clientName = Console.ReadLine();

            Console.WriteLine("Introduce tu dirección");
            this.ClientDirection = Console.ReadLine();

            Console.WriteLine("Introduce tu número de teléfono");
            this.ClientPhoneNumber = Console.ReadLine();


            Console.WriteLine("Para terminar, introduce la cantidad con la que quieres iniciar tu cuenta");
            this.ClientCurrentMoney = Console.ReadLine();
        }

        public void comprobationName()
        {//Menú que pide el nombre después de entrar en la opción de gestionar cuenta
            Console.WriteLine("Has entrado en la opción de gestionar cuenta, ¿Qué deseas hacer?");
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("Pon tu nombre y apellidos para entrar en tu cuenta");
            clientName = Console.ReadLine();
            //Hacer la comprobación de que existe la tabla
        }

        public void changeDetailsAccount()
        {
            Console.WriteLine("Has elegido cambiar los datos de la cuenta");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("¿Qué quieres cambiar?");
            Console.WriteLine("--------------------");
            Console.WriteLine("1 El nombre del dueño de la cuenta");
            Console.WriteLine("2 La dirección");
            Console.WriteLine("3 El número de teléfono");
            int iOptionChangeDetails = Convert.ToInt32(Console.ReadLine());

            switch (iOptionChangeDetails)
            {//Cambiar algunos datos por campo
                case 1:
                    {//Cambiar el nombre 
                        Console.WriteLine("¿Cuál es el nuevo nombre?");
                        string sNewName = Console.ReadLine();
                       
                        Console.WriteLine("El nombre se ha cambiado correctamente");
                        transAccount.addListMovements("Se ha cambiado el nombre de la cuenta a " + sNewName);
                        break;
                    }
                case 2:
                    {//Cambiar la dirección 
                        Console.WriteLine("¿Cuál es la nueva dirección?");
                        string sNewName = Console.ReadLine();
                       
                        Console.WriteLine("La dirección se ha cambiado correctamente");
                        transAccount.addListMovements("Se ha cambiado la dirección de la cuenta a " + sNewName);
                        break;
                    }
                case 3:
                    {//Cambiar el número de teléfono 
                        Console.WriteLine("¿Cuál es el nuevo número de teléfono?");
                        string sNewName = Console.ReadLine();
                      
                        Console.WriteLine("El nombre se ha cambiado correctamente");
                        transAccount.addListMovements("Se ha cambiado número de teléfono de la cuenta a " + sNewName);
                        break;
                    }
            }
            Console.WriteLine("Lsos cambios se han hecho exitosamente.");
            menuAccount.gestionarSalir();
        }


    }
}
