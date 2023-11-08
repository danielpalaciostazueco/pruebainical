using System.Security.Principal;
using bankApp;
using Newtonsoft.Json;
namespace bankApp{

    public class bankTransactions{
        bankAccounts transAccount = new bankAccounts();
        MenuBank menuTrans = new MenuBank(); 
        private List<string> transactions = new List<string>();

        //Constructor
       
        //Método get

    public string getLastTransaction(){// Da la lista de los últimos moviemientos
            return transactions.Last();
        }

        public string getTransactionByPosition(int position) {
            if (transactions.Count <= position) return " ";

            return transactions[position];
        }

        //Método set 

        public void addListMovements(string movement){//Acumula los últimos movimientos
            transactions.Add(movement);
        }

  public void makeDeposit() //case 2 menuOption3
        {
            Console.WriteLine("Has entrado en hacer un ingreso");
            Console.WriteLine("si te has equivocado introduce 1 y seras devuelto al menú de gestionar cuenta");
            string sTrans = Console.ReadLine();

            if (sTrans == "1")
            {//Volver al menú general de gestionar cuenta
           menuTrans.optionGestionar();
            }
            else
            {//Seguir con la transferencia
                Console.WriteLine("¿De cuanto quieres hacer el ingreso?");
                int iTransferDeposit = Convert.ToInt32(Console.ReadLine());
                if (iTransferDeposit < 1)
                {//Para que meta una cantidad positiva
                    Console.WriteLine("Has introducido un número negativo, no puedes poner menos de 1");
                    while (iTransferDeposit < 1)
                    {
                        Console.WriteLine("Prueba otra vez");
                        iTransferDeposit = Convert.ToInt32(Console.ReadLine());
                    }
                }
                checkDestinataryDeposit(iTransferDeposit);//Hace el ingreso
                menuTrans.gestionarSalir();
            }
        }

        public void checkDestinataryDeposit(int i)
        {//comprueba que el destinatario existe para hacer el ingreso
            Console.WriteLine("A quién quieres hacer el ingreso?");
            string sDestinataryDeposit = Console.ReadLine();

            if (db.VerificarExistenciaTablaPorNombre(sDestinataryDeposit))
            {//Comprueba que existe la tabla 
                int iGetMoney = db.ObtenerCampo<int>(sDestinataryDeposit, "CurrentMoney");
                i += iGetMoney;
                db.ActualizarCampo(sDestinataryDeposit, "CurrentMoney", i);
                string s = i.ToString();
                addListMovements("Se ha hecho un ingreso a " + sDestinataryDeposit + " de " + s  + " euros.");
                Console.WriteLine("El ingreso se ha hecho existosamente");
            }
            else
            {//Si no se ha detectado que no existe
                Console.WriteLine("Lo siento, el destinatario no esta en nuestra base de datos o lo has introducido mal");
                Console.WriteLine("------------------------------------------------------------------------------------");
                Console.WriteLine("1 para volver a intentar hacer la transferencia");
                Console.WriteLine("2 para seguir gestionando tu cuenta");
                Console.WriteLine("3 para terminar la operaciones y salir");
                string sOption = Console.ReadLine();
                if (sOption == "2")
                {
                  menuTrans.optionGestionar(); //Entra otra vez en el menú general de gestionar cuenta
                }
                if (sOption == "1")
                {//vuelve a intentar el ingreso
                    Console.WriteLine("Vuelve a poner el importe del ingreso");
                    int x = Convert.ToInt32(Console.ReadLine());
                    checkDestinataryDeposit(x);
                }
                if (sOption == "3")
                {
                    menuTrans.setFinalise(true);
                    Console.WriteLine("Has termiando las operaciones y has salido");
                }

                if (sOption != "1" || sOption != "2" || sOption != "3")
                {//si ha metido mal la opción 
                    Console.WriteLine("Lo has introducido mal");
                    Console.WriteLine("----------------------");
                    Console.WriteLine("1 para volver a hacer el ingreso");
                    Console.WriteLine("2 para seguir gestionando tu cuenta");
                    Console.WriteLine("3 para terminar la operaciones y salir");
                    string sSecondChance = Console.ReadLine();

                    while (sSecondChance != "1" || sSecondChance != "2" || sSecondChance != "3")
                    {//Me aseguro de que lo mete bien
                        Console.WriteLine("Lo has introducido mal");
                        Console.WriteLine("----------------------");
                        Console.WriteLine("1 para volver a el ingreso");
                        Console.WriteLine("2 para seguir gestionando tu cuenta");
                        Console.WriteLine("3 para terminar la operaciones y salir");
                        sSecondChance = Console.ReadLine();
                    }
                    if (sSecondChance == "2")
                    {
                        menuTrans.optionGestionar(); //Entra otra vez en el menú general de gestionar cuenta
                    }
                    if (sSecondChance == "1")
                    {//vuelve a intentar la transferencia
                        Console.WriteLine("Vuelve a poner el importe del ingreso");
                        string x = Console.ReadLine();
                        int j = int.Parse(x);
                        checkDestinataryDeposit(j);
                    }
                    if (sSecondChance == "3")
                    {
                        menuTrans.setFinalise(true);
                        Console.WriteLine("Has termiando las operaciones y has salido");
                    }
                }

            }
        }

     public void withdrawMoney()  //case 3 menuOption3
        {
            Console.WriteLine("Has entrado en sacar dinero");
            Console.WriteLine("si te has equivocado introduce 1 y seras devuelto al menú de gestionar cuenta");
            string sTrans = Console.ReadLine();

            if (sTrans == "1")
            {//Volver al menú general de gestionar cuenta
              menuTrans.optionGestionar();
            }
            else
            {//Seguir para sacar dinero
                Console.WriteLine("¿Cuánto dinero quieres sacar?");
                string sTransferWithdraw = Console.ReadLine();
                int iTransferWithdraw = int.Parse(sTransferWithdraw);
                if (iTransferWithdraw < 1)
                {//Para que meta una cantidad positiva
                    Console.WriteLine("Has introducido un número negativo, no puedes poner menos de 1");
                    while (iTransferWithdraw < 1)
                    {
                        Console.WriteLine("Prueba otra vez");
                        sTransferWithdraw = Console.ReadLine();
                        iTransferWithdraw = int.Parse(sTransferWithdraw);
                    }
                }
                checkDestinataryWithdraw(iTransferWithdraw);//Hace el proceso para sacar dinero
                menuTrans.gestionarSalir();
            }
        }

        public void checkDestinataryWithdraw(int i)
        {
            int iGetMoney = db.ObtenerCampo<int>(clientName, "CurrentMoney");//Obtiene la cantidad para restarla

            if ((iGetMoney - i) > 0)
            {
                iGetMoney -= i;
                string s = i.ToString();
                db.ActualizarCampo(clientName, "CurrentMoney", iGetMoney);//Ingresa el dinero de nuevo ya restado 
                addListMovements("Se ha sacado un importe de " + s + " euros.");
                Console.WriteLine("Proceso existoso");
            }
            else//Si la cantidad que intenta sacar no puede le da opciones
            {
                Console.WriteLine("Lo siento, no puedes sacar tanto dinero");
                Console.WriteLine("------------------------------------------------------------------------------------");
                Console.WriteLine("1 para volver a intentar sacar dinero");
                Console.WriteLine("2 para seguir gestionando tu cuenta");
                Console.WriteLine("3 para terminar la operaciones y salir");
                string sOption = Console.ReadLine();
                if (sOption == "2")
                {
                   menuTrans.optionGestionar(); //Entra otra vez en el menú general de gestionar cuenta
                }
                if (sOption == "1")
                {//vuelve a intentar el ingreso
                    Console.WriteLine("Vuelve a poner el la cantidad que quieres sacar");
                    string x = Console.ReadLine();
                    int j = int.Parse(x);
                    checkDestinataryWithdraw(j);
                }
                if (sOption == "3")
                {
                    menuTrans.setFinalise(true);
                    Console.WriteLine("Has termiando las operaciones y has salido");
                }

                if (sOption != "1" || sOption != "2" || sOption != "3")
                {//si ha metido mal la opción 
                    Console.WriteLine("Lo has introducido mal");
                    Console.WriteLine("----------------------");
                    Console.WriteLine("1 para intentar sacar dinero de nuevo");
                    Console.WriteLine("2 para seguir gestionando tu cuenta");
                    Console.WriteLine("3 para terminar la operaciones y salir");
                    string sSecondChance = Console.ReadLine();

                    while (sSecondChance != "1" || sSecondChance != "2" || sSecondChance != "3")
                    {//Me aseguro de que lo mete bien
                        Console.WriteLine("Lo has introducido mal");
                        Console.WriteLine("----------------------");
                        Console.WriteLine("1 para volver a sacar dinero");
                        Console.WriteLine("2 para seguir gestionando tu cuenta");
                        Console.WriteLine("3 para terminar la operaciones y salir");
                        sSecondChance = Console.ReadLine();
                    }
                    if (sSecondChance == "2")
                    {
                        menuTrans.optionGestionar(); //Entra otra vez en el menú general de gestionar cuenta
                    }
                    if (sSecondChance == "1")
                    {//vuelve a intentar la transferencia
                        Console.WriteLine("Vuelve a poner la cantidad que quieres sacar");
                        string x = Console.ReadLine();
                        int j = int.Parse(x);
                        checkDestinataryWithdraw(j);
                    }
                    if (sSecondChance == "3")
                    {
                        menuTrans.setFinalise(true);
                        Console.WriteLine("Has termiando las operaciones y has salido");
                    }
                }
            }
        }

        public void makeTransfer()//case 4 menuOption3
        {
            Console.WriteLine("Has entrado en hacer una tranferencia");
            Console.WriteLine("si te has equivocado introduce 1 y seras devuelto al menú de gestionar cuenta");
            string sTrans = Console.ReadLine();

            if (sTrans == "1")
            {//Volver al menú general de gestionar cuenta
                menuTrans.optionGestionar();
            }
            else
            {//seguir para hacer la transferencia
                Console.WriteLine("¿De cuanto quieres hacer la transferencia?");
                string sTransAmount = Console.ReadLine();
                int iTransAmount = int.Parse(sTransAmount);
                Console.WriteLine("¿A quién quieres hacer la transferencia?");
                string sDestinataryTransfer = Console.ReadLine();

                if (db.VerificarExistenciaTablaPorNombre(sDestinataryTransfer))//Verifica si existe el destinatario
                {//Verifica si el destinatario existe
                    int iGetMoneyeDestinatary = db.ObtenerCampo<int>(sDestinataryTransfer, "CurrentMoney"); //obtengo la cantidad del destinatario
                    int iGetMoneyOrigin = db.ObtenerCampo<int>(clientName, "CurrentMoney");//Obtengo la cantidad del origen              
                    //proceso para hacer la transferencia de una cuenta a otra
                    if (iGetMoneyOrigin - iTransAmount < 0)//Si lo que tiene es menos de lo que quiere transferir doy diferentes opciones
                    {
                        Console.WriteLine("Lo siento, no puedes hacer la transferencia porque no tienes tanto dinero en tu cuenta");
                        Console.WriteLine("------------------------------------------------------------------------------------");
                        Console.WriteLine("1 para intentar ha hacer la transferencia");
                        Console.WriteLine("2 para seguir gestionando tu cuenta");
                        Console.WriteLine("3 para terminar la operaciones y salir");
                        string sOption = Console.ReadLine();
                        if (sOption == "2")
                        {
                            menuTrans.optionGestionar(); //Entra otra vez en el menú general de gestionar cuenta
                        }
                        if (sOption == "1")
                        {//vuelve a intentar hacer la transferencia
                            makeTransfer();
                        }
                        if (sOption == "3")
                        {
                            menuTrans.setFinalise(true);
                            Console.WriteLine("Has termiando las operaciones y has salido");
                        }

                        if (sOption != "1" || sOption != "2" || sOption != "3")//me aseguro de que introduzca  1 , 2 o 3
                        {//si ha metido mal la opción 
                            Console.WriteLine("Lo has introducido mal");
                            Console.WriteLine("----------------------");
                            Console.WriteLine("1 para intentar hacer la transferencia de nuevo");
                            Console.WriteLine("2 para seguir gestionando tu cuenta");
                            Console.WriteLine("3 para terminar la operaciones y salir");
                            string sSecondChance = Console.ReadLine();

                            while (sSecondChance != "1" || sSecondChance != "2" || sSecondChance != "3")
                            {//Me aseguro de que lo mete bien
                                Console.WriteLine("Lo has introducido mal");
                                Console.WriteLine("----------------------");
                                Console.WriteLine("1 para intentar hacer la transferencia");
                                Console.WriteLine("2 para seguir gestionando tu cuenta");
                                Console.WriteLine("3 para terminar la operaciones y salir");
                                sSecondChance = Console.ReadLine();
                            }
                            if (sSecondChance == "2")
                            {
                               menuTrans.optionGestionar(); //Entra otra vez en el menú general de gestionar cuenta
                            }
                            if (sSecondChance == "1")
                            {//vuelve a intentar la transferencia
                               makeTransfer();                            
                            }
                            if (sSecondChance == "3")
                            {//finaliza las operaciones y se termina el programa
                                menuTrans.setFinalise(true);
                                Console.WriteLine("Has termiando las operaciones y has salido");
                            }
                        }
                    }else{//La cantidad que quiere transferir es menor a lo que tiene en la cuenta a si que puede seguir con el proceso   
                        iGetMoneyeDestinatary += iTransAmount;
                        iGetMoneyOrigin -= iTransAmount;
                        string i = iTransAmount.ToString();
                        //introduzco el dinero de las transferencias
                        db.ActualizarCampo(sDestinataryTransfer,"CurrentMoney",iGetMoneyeDestinatary);//al destinatario
                        db.ActualizarCampo(clientName,"CurrentMoney", iGetMoneyOrigin);//el origen
                        addListMovements("Se ha transferido " + i + " euros de " + menuTrans.+ " a " + sDestinataryTransfer);
                        Console.WriteLine("La transferencia se ha realizado correctamente.");
                        menuTrans.gestionarSalir();
                    }
                }
            }
            


    }
}