using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Projeto02_GestorDeClientes_CMD
{
    class Program
    {
        [System.Serializable] // Permite que salve dados desse tipo dentro de arquivos
        struct Cliente
        {
          public string nome;
          public string email;
          public string cpf;
        }

        static List<Cliente> clientes = new List<Cliente>(); // Lista vazia

        enum Menu{Listagem = 1, Adicionar = 2, Remover = 3, Sair = 4}

        static void Main(string[] args)
        {
            Carregar();
            bool escolheuSair = false;
            while(!escolheuSair)
            {
                //Entrada
                Console.WriteLine("Sistema de clientes Bem-Vindo!");
                Console.WriteLine("1-Listagem\n2-Adicionar\n3-Remover\n4-Sair");
                int intOpcao = int.Parse(Console.ReadLine());
                Menu opcao = (Menu)intOpcao; //Convertendo a entrsda para um tipo enum(int)

                //Processando a entrada do usuário
                switch(opcao)
                {
                    case Menu.Listagem:
                        Listagem();
                        break;
                    case Menu.Adicionar:
                        Adicionar();
                        break;
                    case Menu.Remover:
                        Remover();
                        break;
                    case Menu.Sair:
                        escolheuSair = true;
                        break;
                }
                Console.Clear();
            }
        }
        static void Adicionar()
        {
            Cliente cliente = new Cliente();
            Console.WriteLine("Cadastro de cliente:");
            Console.WriteLine("Nome do cliente:");
            cliente.nome = Console.ReadLine();
            Console.WriteLine("Email do cliente:");
            cliente.email = Console.ReadLine();
            Console.WriteLine("CPF do cliente:");
            cliente.cpf = Console.ReadLine();

            clientes.Add(cliente);

            Salvar();

            Console.WriteLine("Cadastro concluído, aperte ENTER para sair.");
            Console.ReadLine();
        }
        static void Listagem()
        {
            if (clientes.Count > 0) // SE tem pelo menos um cliente
            {
                int i = 0;
                foreach(Cliente cliente in clientes)
                {
                    Console.WriteLine($"ID:{i}");
                    Console.WriteLine($"NOME:{cliente.nome}");
                    Console.WriteLine($"E-MAIL:{cliente.email}");
                    Console.WriteLine($"CPF:{cliente.cpf}");
                    Console.WriteLine("========================");
                    i++;
                }
            }
            else
            {
                Console.WriteLine("Nenhum cliente cadastrado.");
            }
            Console.WriteLine("Aperte ENTER para sair.");
            Console.ReadLine();
        }
        static void Remover()
        {
            Listagem();
            Console.WriteLine("Digite o ID do cliente que você quer remover:");
            int id = int.Parse(Console.ReadLine());
            
            if (id >= 0 && id < clientes.Count) // O número tem que o usuário digitar tem que ser positivo E menor que o tamanho da lista.
            {
                clientes.RemoveAt(id);
                Salvar();

                Console.WriteLine("Usuário removido com sucesso!");
                Console.WriteLine("Pressione ENTER para sair!");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("ID digitado é inválido, tente novamente!");
                Console.ReadLine();
            }
        }
        static void Salvar()
        {
            FileStream stream = new FileStream("clients.dat", FileMode.OpenOrCreate);
            BinaryFormatter enconder = new BinaryFormatter();
            
            enconder.Serialize(stream, clientes);

            stream.Close();
        }
        static void Carregar()
        {
            FileStream stream = new FileStream("clients.dat", FileMode.OpenOrCreate);

            try // Tentar
            {
                BinaryFormatter enconder = new BinaryFormatter();
            
                clientes = (List<Cliente>)enconder.Deserialize(stream);

                stream.Close();
                if(clientes == null)
                {
                    clientes = new List<Cliente>();
                }
            }
            catch(Exception)
            {
                clientes = new List<Cliente>();
            }
            stream.Close();
        }
    }
}