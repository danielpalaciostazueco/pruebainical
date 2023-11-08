// See https://aka.ms/new-console-template for more information
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Serialization;
using bankApp;



internal class Program
{
    
     
    private static void Main(string[] args){

        bankAccounts bAccount = new bankAccounts();
        bankTransactions btrans = new bankTransactions();
        MenuBank menuB = new MenuBank();
        Database dtb = new Database();
       
        
        
        menuB.setFinalise(false);
        while (menuB.getFinalise() != true){
            int option = menuB.MenuConsola();
            if (option == 1)//Crear cuenta
            {
                bAccount.createAccount();//Introduce la información.
                //Crea la tabla con los datos.
                
                Console.WriteLine("¿Quieres empezar a gestionar la cuenta o salir?");
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine("Escribe 1 para gestionar la cuenta o 2 para salir");
                string sGestionaroSalir = Console.ReadLine();
                int iGestionaroSalir = int.Parse(sGestionaroSalir);
                if(iGestionaroSalir == 1){
                    menuB.MenuOption2();
                    menuB.MenuOption3();//hacer lo mismo
                }else if(iGestionaroSalir == 2){
                    menuB.setFinalise(true);
                    Console.WriteLine("Has salido existosamente!!!!");
                }
            }
            if(option == 2){//Gestionar cuenta
                menuB.MenuOption2();//Introduce el nombre 
                menuB.MenuOption3();//Menú opciones de gestionar cuenta
                
                   
            }




        }
    }
}