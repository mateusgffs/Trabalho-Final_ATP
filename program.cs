using System;
using System.IO;

class Program
{
    // Vetores para armazenar os nomes dos produtos e a quantidade em estoque
    static string[] produtos = new string[4];
    static int[] estoque = new int[4];
    // Matriz para registrar as vendas diárias durante um mês (30 dias)
    static int[,] vendas = new int[30, 4];

    static void Main(string[] args)
    {
        int opcao = 0;
        // Loop principal do menu
        do
        {
            Console.WriteLine("Menu Principal");
            Console.WriteLine("1 – Importar arquivo de produtos");
            Console.WriteLine("2 – Registrar venda");
            Console.WriteLine("3 – Relatório de vendas");
            Console.WriteLine("4 – Relatório de estoque");
            Console.WriteLine("5 – Criar arquivo de vendas");
            Console.WriteLine("6 - Sair");
            Console.Write("Escolha uma opção: ");
            opcao = int.Parse(Console.ReadLine());

            // Seleção da ação com base na opção do menu
            switch (opcao)
            {
                case 1:
                    ImportarProdutos();
                    break;
                case 2:
                    RegistrarVenda();
                    break;
                case 3:
                    RelatorioVendas();
                    break;
                case 4:
                    RelatorioEstoque();
                    break;
                case 5:
                    CriarArquivoVendas();
                    break;
                case 6:
                    Console.WriteLine("Saindo...");
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        } while (opcao != 6);
    }

    // Método para importar os produtos de um arquivo
    static void ImportarProdutos()
    {
        // Tenta ler o arquivo "produtos.txt" e carregar os dados nos vetores
        try
        {
            string[] linhas = File.ReadAllLines("produtos.txt");
            for (int i = 0; i < 4; i++)
            {
                string[] partes = linhas[i].Split(' ');
                produtos[i] = partes[0];
                estoque[i] = int.Parse(partes[1]);
            }
            Console.WriteLine("Produtos importados com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao importar produtos: " + ex.Message);
        }
    }

    // Método para registrar uma venda
    static void RegistrarVenda()
    {
        // Solicita o número do produto, dia do mês e quantidade vendida
        Console.Write("Digite o número do produto (0 a 3): ");
        int produto = int.Parse(Console.ReadLine());
        Console.Write("Digite o dia do mês (1 a 30): ");
        int dia = int.Parse(Console.ReadLine());
        Console.Write("Digite a quantidade vendida: ");
        int quantidade = int.Parse(Console.ReadLine());

        // Verifica se as entradas são válidas
        if (produto < 0 || produto >= 4 || dia < 1 || dia > 30 || quantidade < 0)
        {
            Console.WriteLine("Entrada inválida.");
            return;
        }

        // Verifica se há estoque suficiente para a venda
        if (quantidade <= estoque[produto])
        {
            vendas[dia - 1, produto] += quantidade; // Registra a venda
            estoque[produto] -= quantidade; // Atualiza o estoque
            Console.WriteLine("Venda registrada com sucesso.");
        }
        else
        {
            Console.WriteLine("Quantidade de vendas ultrapassa o estoque disponível.");
        }
    }

    // Método para exibir o relatório de vendas
    static void RelatorioVendas()
    {
        Console.WriteLine("Relatório de Vendas:");
        Console.WriteLine("Dia\tProduto A\tProduto B\tProduto C\tProduto D");
        // Loop para cada dia do mês
        for (int dia = 0; dia < 30; dia++)
        {
            Console.Write(dia + 1 + "\t");
            // Loop para cada produto
            for (int produto = 0; produto < 4; produto++)
            {
                Console.Write(vendas[dia, produto] + "\t\t");
            }
            Console.WriteLine();
        }
    }

    // Método para exibir o relatório de estoque
    static void RelatorioEstoque()
    {
        Console.WriteLine("Relatório de Estoque Atualizado:");
        // Loop para cada produto
        for (int i = 0; i < 4; i++)
        {
            Console.WriteLine(produtos[i] + " " + estoque[i]);
        }
    }

    // Método para criar um arquivo com o total de vendas por produto
    static void CriarArquivoVendas()
    {
        try
        {
            // Usando StreamWriter para criar e escrever no arquivo "total_vendas.txt"
            using (StreamWriter sw = new StreamWriter("total_vendas.txt"))
            {
                // Loop para cada produto
                for (int i = 0; i < 4; i++)
                {
                    int totalVendas = 0;
                    // Loop para cada dia do mês
                    for (int dia = 0; dia < 30; dia++)
                    {
                        totalVendas += vendas[dia, i];
                    }
                    sw.WriteLine(produtos[i] + " " + totalVendas);
                }
            }
            Console.WriteLine("Arquivo de vendas criado com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao criar arquivo de vendas: " + ex.Message);
        }
    }
}
