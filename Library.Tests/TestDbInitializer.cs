using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Data;

namespace Library.Tests
{
    public class TestDbInitializer
    {
        public static void Initialize(LibraryContext context, string imageDirectory)
        {
            #region Books

            var books = new List<Book>();

            var small = Path.Combine(imageDirectory, "a_furedi_lany_thumb.jpg");
            var large = Path.Combine(imageDirectory, "a_furedi_lany.jpg");

            if (File.Exists(small) && File.Exists(large))
            {
                books.Add(new Book
                {
                    Title = "A füredi lány",
                    Writer = "Karády Anna",
                    Year = 2022,
                    ISBNNumber = "9789635700295",
                    PopularityNumber = 0,
                    SmallCoverImage = File.ReadAllBytes(small),
                    NormalCoverImage = File.ReadAllBytes(large)
                });
            }

            small = Path.Combine(imageDirectory, "a_fuvesasszony_thumb.jpg");
            large = Path.Combine(imageDirectory, "a_fuvesasszony.jpg");

            if (File.Exists(small) && File.Exists(large))
            {
                books.Add(new Book
                {
                    Title = "A Füvesasszony",
                    Writer = "Philippa Gregory",
                    Year = 2022,
                    ISBNNumber = "9789636040055",
                    PopularityNumber = 0,
                    SmallCoverImage = File.ReadAllBytes(small),
                    NormalCoverImage = File.ReadAllBytes(large)
                });
            }

            small = Path.Combine(imageDirectory, "a_kingfisher_hill-i_gyilkossagok_thumb.jpg");
            large = Path.Combine(imageDirectory, "a_kingfisher_hill-i_gyilkossagok.jpg");

            if (File.Exists(small) && File.Exists(large))
            {
                books.Add(new Book
                {
                    Title = "A Kingfisher hill-i gyilkosságok",
                    Writer = "Agatha Christie",
                    Year = 2022,
                    ISBNNumber = "9789634794899",
                    PopularityNumber = 0,
                    SmallCoverImage = File.ReadAllBytes(small),
                    NormalCoverImage = File.ReadAllBytes(large)
                });
            }

            small = Path.Combine(imageDirectory, "a_koszivu_ember_fiai_thumb.jpg");
            large = Path.Combine(imageDirectory, "a_koszivu_ember_fiai.jpg");

            if (File.Exists(small) && File.Exists(large))
            {
                books.Add(new Book
                {
                    Title = "A kőszívű ember fiai",
                    Writer = "Jókai Mór",
                    Year = 2015,
                    ISBNNumber = "9786066469166",
                    PopularityNumber = 0,
                    SmallCoverImage = File.ReadAllBytes(small),
                    NormalCoverImage = File.ReadAllBytes(large)
                });
            }

            small = Path.Combine(imageDirectory, "a_pal_utcai_fiuk_thumb.jpg");
            large = Path.Combine(imageDirectory, "a_pal_utcai_fiuk.jpg");

            if (File.Exists(small) && File.Exists(large))
            {
                books.Add(new Book
                {
                    Title = "A Pál utcai fiúk",
                    Writer = "Molnár Ferenc",
                    Year = 2021,
                    ISBNNumber = "9789634865292",
                    PopularityNumber = 0,
                    SmallCoverImage = File.ReadAllBytes(small),
                    NormalCoverImage = File.ReadAllBytes(large)
                });
            }

            small = Path.Combine(imageDirectory, "addig_se_iszik_thumb.jpg");
            large = Path.Combine(imageDirectory, "addig_se_iszik.jpg");

            if (File.Exists(small) && File.Exists(large))
            {
                books.Add(new Book
                {
                    Title = "Addig se iszik",
                    Writer = "Bödőcs Tibor",
                    Year = 2022,
                    ISBNNumber = "9789634798101",
                    PopularityNumber = 0,
                    SmallCoverImage = File.ReadAllBytes(small),
                    NormalCoverImage = File.ReadAllBytes(large)
                });
            }

            small = Path.Combine(imageDirectory, "amit_a_magyar_uralkodokrol_tudni_illik_thumb.jpg");
            large = Path.Combine(imageDirectory, "amit_a_magyar_uralkodokrol_tudni_illik.jpg");

            if (File.Exists(small) && File.Exists(large))
            {
                books.Add(new Book
                {
                    Title = "Amit a magyar uralkodókról tudni illik",
                    Writer = "TKK Kereskedelmi Kft.",
                    Year = 2022,
                    ISBNNumber = "9789635101887",
                    PopularityNumber = 0,
                    SmallCoverImage = File.ReadAllBytes(small),
                    NormalCoverImage = File.ReadAllBytes(large)
                });
            }

            small = Path.Combine(imageDirectory, "androidos_alkalmazasfejlesztes_kotlin_nyelven_1_thumb.jpg");
            large = Path.Combine(imageDirectory, "androidos_alkalmazasfejlesztes_kotlin_nyelven_1.jpg");

            if (File.Exists(small) && File.Exists(large))
            {
                books.Add(new Book
                {
                    Title = "Androidos alkalmazásfejlesztés Kotlin nyelven - 1. rész",
                    Writer = "Fehér Krisztián",
                    Year = 2021,
                    ISBNNumber = "9786156184009",
                    PopularityNumber = 0,
                    SmallCoverImage = File.ReadAllBytes(small),
                    NormalCoverImage = File.ReadAllBytes(large)
                });
            }

            small = Path.Combine(imageDirectory, "androidos_alkalmazasfejlesztes_kotlin_nyelven_2_thumb.jpg");
            large = Path.Combine(imageDirectory, "androidos_alkalmazasfejlesztes_kotlin_nyelven_2.jpg");

            if (File.Exists(small) && File.Exists(large))
            {
                books.Add(new Book
                {
                    Title = "Androidos alkalmazásfejlesztés Kotlin nyelven - 2. rész",
                    Writer = "Fehér Krisztián",
                    Year = 2021,
                    ISBNNumber = "9786156184054",
                    PopularityNumber = 0,
                    SmallCoverImage = File.ReadAllBytes(small),
                    NormalCoverImage = File.ReadAllBytes(large)
                });
            }

            small = Path.Combine(imageDirectory, "androidos_alkalmazasfejlesztes_kotlin_nyelven_3_thumb.jpg");
            large = Path.Combine(imageDirectory, "androidos_alkalmazasfejlesztes_kotlin_nyelven_3.jpg");

            if (File.Exists(small) && File.Exists(large))
            {
                books.Add(new Book
                {
                    Title = "Androidos alkalmazásfejlesztés Kotlin nyelven - 3. rész",
                    Writer = "Fehér Krisztián",
                    Year = 2022,
                    ISBNNumber = "9786156184184",
                    PopularityNumber = 0,
                    SmallCoverImage = File.ReadAllBytes(small),
                    NormalCoverImage = File.ReadAllBytes(large)
                });
            }

            small = Path.Combine(imageDirectory, "angol-magyar_szotar_thumb.jpg");
            large = Path.Combine(imageDirectory, "angol-magyar_szotar.jpg");

            if (File.Exists(small) && File.Exists(large))
            {
                books.Add(new Book
                {
                    Title = "Angol-magyar szótár",
                    Writer = "Magay Tamás",
                    Year = 2018,
                    ISBNNumber = "9789630598941",
                    PopularityNumber = 0,
                    SmallCoverImage = File.ReadAllBytes(small),
                    NormalCoverImage = File.ReadAllBytes(large)
                });
            }

            small = Path.Combine(imageDirectory, "antigone_thumb.jpg");
            large = Path.Combine(imageDirectory, "antigone.jpg");

            if (File.Exists(small) && File.Exists(large))
            {
                books.Add(new Book
                {
                    Title = "Antigoné",
                    Writer = "Szophoklész",
                    Year = 2015,
                    ISBNNumber = "9786066468657",
                    PopularityNumber = 0,
                    SmallCoverImage = File.ReadAllBytes(small),
                    NormalCoverImage = File.ReadAllBytes(large)
                });
            }

            small = Path.Combine(imageDirectory, "antikvar_konyv-egy_sohajtasnyi_ido_thumb.jpg");
            large = Path.Combine(imageDirectory, "antikvar_konyv-egy_sohajtasnyi_ido.jpg");

            if (File.Exists(small) && File.Exists(large))
            {
                books.Add(new Book
                {
                    Title = "Antikvár könyv - Egy sóhajtásnyi idő",
                    Writer = "Anne Philipe",
                    Year = 1991,
                    ISBNNumber = "9630752174",
                    PopularityNumber = 0,
                    SmallCoverImage = File.ReadAllBytes(small),
                    NormalCoverImage = File.ReadAllBytes(large)
                });
            }

            small = Path.Combine(imageDirectory, "antikvar_konyv-garas_thumb.jpg");
            large = Path.Combine(imageDirectory, "antikvar_konyv-garas.jpg");

            if (File.Exists(small) && File.Exists(large))
            {
                books.Add(new Book
                {
                    Title = "Antikvár könyv - Garas",
                    Writer = "Tarján Tamás",
                    Year = 1991,
                    ISBNNumber = "9635644698",
                    PopularityNumber = 0,
                    SmallCoverImage = File.ReadAllBytes(small),
                    NormalCoverImage = File.ReadAllBytes(large)
                });
            }

            small = Path.Combine(imageDirectory, "az_elso_vilaghaboru_tortenete_thumb.jpg");
            large = Path.Combine(imageDirectory, "az_elso_vilaghaboru_tortenete.jpg");

            if (File.Exists(small) && File.Exists(large))
            {
                books.Add(new Book
                {
                    Title = "Az első világháború története",
                    Writer = "R. Kovács Anna",
                    Year = 2022,
                    ISBNNumber = "9789639631595",
                    PopularityNumber = 0,
                    SmallCoverImage = File.ReadAllBytes(small),
                    NormalCoverImage = File.ReadAllBytes(large)
                });
            }

            small = Path.Combine(imageDirectory, "az_ember_tragediaja_thumb.jpg");
            large = Path.Combine(imageDirectory, "az_ember_tragediaja.jpg");

            if (File.Exists(small) && File.Exists(large))
            {
                books.Add(new Book
                {
                    Title = "Az ember tragédiája",
                    Writer = "Madách Imre",
                    Year = 2021,
                    ISBNNumber = "9786066468701",
                    PopularityNumber = 0,
                    SmallCoverImage = File.ReadAllBytes(small),
                    NormalCoverImage = File.ReadAllBytes(large)
                });
            }

            small = Path.Combine(imageDirectory, "az_uj_lany_thumb.jpg");
            large = Path.Combine(imageDirectory, "az_uj_lany.jpg");

            if (File.Exists(small) && File.Exists(large))
            {
                books.Add(new Book
                {
                    Title = "Az új lány",
                    Writer = "Silva Daniel",
                    Year = 2020,
                    ISBNNumber = "9789635401208",
                    PopularityNumber = 0,
                    SmallCoverImage = File.ReadAllBytes(small),
                    NormalCoverImage = File.ReadAllBytes(large)
                });
            }

            small = Path.Combine(imageDirectory, "bank_ban_thumb.jpg");
            large = Path.Combine(imageDirectory, "bank_ban.jpg");

            if (File.Exists(small) && File.Exists(large))
            {
                books.Add(new Book
                {
                    Title = "Bánk Bán",
                    Writer = "Katona józsef",
                    Year = 2019,
                    ISBNNumber = "9789632763262",
                    PopularityNumber = 0,
                    SmallCoverImage = File.ReadAllBytes(small),
                    NormalCoverImage = File.ReadAllBytes(large)
                });
            }

            small = Path.Combine(imageDirectory, "buszkeseg_es_balitelet_thumb.jpg");
            large = Path.Combine(imageDirectory, "buszkeseg_es_balitelet.jpg");

            if (File.Exists(small) && File.Exists(large))
            {
                books.Add(new Book
                {
                    Title = "Büszkeség és balítélet",
                    Writer = "Austen Jane",
                    Year = 2020,
                    ISBNNumber = "9789634038306",
                    PopularityNumber = 0,
                    SmallCoverImage = File.ReadAllBytes(small),
                    NormalCoverImage = File.ReadAllBytes(large)
                });
            }

            small = Path.Combine(imageDirectory, "edes_anna_thumb.jpg");
            large = Path.Combine(imageDirectory, "edes_anna.jpg");

            if (File.Exists(small) && File.Exists(large))
            {
                books.Add(new Book
                {
                    Title = "Édes Anna",
                    Writer = "Kosztolányi Dezső",
                    Year = 2016,
                    ISBNNumber = "9789632763200",
                    PopularityNumber = 0,
                    SmallCoverImage = File.ReadAllBytes(small),
                    NormalCoverImage = File.ReadAllBytes(large)
                });
            }

            small = Path.Combine(imageDirectory, "egri_csillagok_thumb.jpg");
            large = Path.Combine(imageDirectory, "egri_csillagok.jpg");

            if (File.Exists(small) && File.Exists(large))
            {
                books.Add(new Book
                {
                    Title = "Egri csillagok",
                    Writer = "Gárdonyi Géza",
                    Year = 2016,
                    ISBNNumber = "9786066469173",
                    PopularityNumber = 0,
                    SmallCoverImage = File.ReadAllBytes(small),
                    NormalCoverImage = File.ReadAllBytes(large)
                });
            }

            small = Path.Combine(imageDirectory, "egy_vilagot_felforgato_szerelem_tortenete_thumb.jpg");
            large = Path.Combine(imageDirectory, "egy_vilagot_felforgato_szerelem_tortenete.jpg");

            if (File.Exists(small) && File.Exists(large))
            {
                books.Add(new Book
                {
                    Title = "Egy világot felforgató szerelem története",
                    Writer = "Benedict Marie",
                    Year = 2020,
                    ISBNNumber = "9789636040062",
                    PopularityNumber = 0,
                    SmallCoverImage = File.ReadAllBytes(small),
                    NormalCoverImage = File.ReadAllBytes(large)
                });
            }

            small = Path.Combine(imageDirectory, "eldobhato_testek_thumb.jpg");
            large = Path.Combine(imageDirectory, "eldobhato_testek.jpg");

            if (File.Exists(small) && File.Exists(large))
            {
                books.Add(new Book
                {
                    Title = "Eldobható testek",
                    Writer = "Brandon Hackett",
                    Year = 2022,
                    ISBNNumber = "9789634197546",
                    PopularityNumber = 0,
                    SmallCoverImage = File.ReadAllBytes(small),
                    NormalCoverImage = File.ReadAllBytes(large)
                });
            }


            small = Path.Combine(imageDirectory, "hackertechnikak_thumb.jpg");
            large = Path.Combine(imageDirectory, "hackertechnikak.jpg");

            if (File.Exists(small) && File.Exists(large))
            {
                books.Add(new Book
                {
                    Title = "Hackertechnikák",
                    Writer = "Fehér Krisztián",
                    Year = 2018,
                    ISBNNumber = "9786155477645",
                    PopularityNumber = 0,
                    SmallCoverImage = File.ReadAllBytes(small),
                    NormalCoverImage = File.ReadAllBytes(large)
                });
            }


            small = Path.Combine(imageDirectory, "halal_a_niluson_thumb.jpg");
            large = Path.Combine(imageDirectory, "halal_a_niluson.jpg");

            if (File.Exists(small) && File.Exists(large))
            {
                books.Add(new Book
                {
                    Title = "Halál a Níluson",
                    Writer = "Agatha Christie",
                    Year = 2022,
                    ISBNNumber = "9789634795353",
                    PopularityNumber = 0,
                    SmallCoverImage = File.ReadAllBytes(small),
                    NormalCoverImage = File.ReadAllBytes(large)
                });
            }


            small = Path.Combine(imageDirectory, "harcosok_klubja_thumb.jpg");
            large = Path.Combine(imageDirectory, "harcosok_klubja.jpg");

            if (File.Exists(small) && File.Exists(large))
            {
                books.Add(new Book
                {
                    Title = "Harcosok klubja",
                    Writer = "Chuck Palahniuk",
                    Year = 2022,
                    ISBNNumber = "9789634794981",
                    PopularityNumber = 0,
                    SmallCoverImage = File.ReadAllBytes(small),
                    NormalCoverImage = File.ReadAllBytes(large)
                });
            }

            small = Path.Combine(imageDirectory, "hitler_utolso_dobasa_thumb.jpg");
            large = Path.Combine(imageDirectory, "hitler_utolso_dobasa.jpg");

            if (File.Exists(small) && File.Exists(large))
            {
                books.Add(new Book
                {
                    Title = "Hitler utolsó dobása",
                    Writer = "Jeremy Dronfield",
                    Year = 2022,
                    ISBNNumber = "9789635045495",
                    PopularityNumber = 0,
                    SmallCoverImage = File.ReadAllBytes(small),
                    NormalCoverImage = File.ReadAllBytes(large)
                });
            }

            small = Path.Combine(imageDirectory, "hogyan_valj_jol_fizetett_csharp_programozova_thumb.jpg");
            large = Path.Combine(imageDirectory, "hogyan_valj_jol_fizetett_csharp_programozova.jpg");

            if (File.Exists(small) && File.Exists(large))
            {
                books.Add(new Book
                {
                    Title = "Hogyan válj jól fizetett C# programozóvá?",
                    Writer = "Koncz Balázs",
                    Year = 2019,
                    ISBNNumber = "9786155477690",
                    PopularityNumber = 0,
                    SmallCoverImage = File.ReadAllBytes(small),
                    NormalCoverImage = File.ReadAllBytes(large)
                });
            }

            small = Path.Combine(imageDirectory, "janos_vitez_thumb.jpg");
            large = Path.Combine(imageDirectory, "janos_vitez.jpg");

            if (File.Exists(small) && File.Exists(large))
            {
                books.Add(new Book
                {
                    Title = "János Vitéz",
                    Writer = "Petőfi Sándor",
                    Year = 2016,
                    ISBNNumber = "9789633468838",
                    PopularityNumber = 0,
                    SmallCoverImage = File.ReadAllBytes(small),
                    NormalCoverImage = File.ReadAllBytes(large)
                });
            }

            small = Path.Combine(imageDirectory, "java_thumb.jpg");
            large = Path.Combine(imageDirectory, "java.jpg");

            if (File.Exists(small) && File.Exists(large))
            {
                books.Add(new Book
                {
                    Title = "Java",
                    Writer = "Barry Burd",
                    Year = 2017,
                    ISBNNumber = "9786155186523",
                    PopularityNumber = 0,
                    SmallCoverImage = File.ReadAllBytes(small),
                    NormalCoverImage = File.ReadAllBytes(large)
                });
            }

            small = Path.Combine(imageDirectory, "kisasszonyok_thumb.jpg");
            large = Path.Combine(imageDirectory, "kisasszonyok.jpg");

            if (File.Exists(small) && File.Exists(large))
            {
                books.Add(new Book
                {
                    Title = "Kisasszonyok",
                    Writer = "Louisa May Alcott",
                    Year = 2022,
                    ISBNNumber = "9789632675572",
                    PopularityNumber = 0,
                    SmallCoverImage = File.ReadAllBytes(small),
                    NormalCoverImage = File.ReadAllBytes(large)
                });
            }

            small = Path.Combine(imageDirectory, "latin_magyar_alapszotar_thumb.jpg");
            large = Path.Combine(imageDirectory, "latin_magyar_alapszotar.jpg");

            if (File.Exists(small) && File.Exists(large))
            {
                books.Add(new Book
                {
                    Title = "Latin-magyar alapszótár",
                    Writer = "Goreczky Zsolt",
                    Year = 2022,
                    ISBNNumber = "9786155219610",
                    PopularityNumber = 0,
                    SmallCoverImage = File.ReadAllBytes(small),
                    NormalCoverImage = File.ReadAllBytes(large)
                });
            }

            small = Path.Combine(imageDirectory, "magyar-nemet_kisszotar_thumb.jpg");
            large = Path.Combine(imageDirectory, "magyar-nemet_kisszotar.jpg");

            if (File.Exists(small) && File.Exists(large))
            {
                books.Add(new Book
                {
                    Title = "Magyar-német kisszótár",
                    Writer = "Pozmányi Gyöngyi",
                    Year = 2022,
                    ISBNNumber = "9789634547372",
                    PopularityNumber = 0,
                    SmallCoverImage = File.ReadAllBytes(small),
                    NormalCoverImage = File.ReadAllBytes(large)
                });
            }


            small = Path.Combine(imageDirectory, "mely_levego_thumb.jpg");
            large = Path.Combine(imageDirectory, "mely_levego.jpg");

            if (File.Exists(small) && File.Exists(large))
            {
                books.Add(new Book
                {
                    Title = "Mély levegő",
                    Writer = "Halász Rita",
                    Year = 2022,
                    ISBNNumber = "9789635182268",
                    PopularityNumber = 0,
                    SmallCoverImage = File.ReadAllBytes(small),
                    NormalCoverImage = File.ReadAllBytes(large)
                });
            }

            small = Path.Combine(imageDirectory, "odusszeia_thumb.jpg");
            large = Path.Combine(imageDirectory, "odusszeia.jpg");

            if (File.Exists(small) && File.Exists(large))
            {
                books.Add(new Book
                {
                    Title = "Odüsszeia",
                    Writer = "Homérosz",
                    Year = 2018,
                    ISBNNumber = "9789634052630",
                    PopularityNumber = 0,
                    SmallCoverImage = File.ReadAllBytes(small),
                    NormalCoverImage = File.ReadAllBytes(large)
                });
            }

            small = Path.Combine(imageDirectory, "programozz_c_nyelven_thumb.jpg");
            large = Path.Combine(imageDirectory, "programozz_c_nyelven.jpg");

            if (File.Exists(small) && File.Exists(large))
            {
                books.Add(new Book
                {
                    Title = "Programozz C nyelven!",
                    Writer = "Fehér Krisztián",
                    Year = 2022,
                    ISBNNumber = "9786156184177",
                    PopularityNumber = 0,
                    SmallCoverImage = File.ReadAllBytes(small),
                    NormalCoverImage = File.ReadAllBytes(large)
                });
            }

            small = Path.Combine(imageDirectory, "romeo_es_julia_thumb.jpg");
            large = Path.Combine(imageDirectory, "romeo_es_julia.jpg");

            if (File.Exists(small) && File.Exists(large))
            {
                books.Add(new Book
                {
                    Title = "Rómeó és Júlia",
                    Writer = "William Shakespeare",
                    Year = 2021,
                    ISBNNumber = "9789632452036",
                    PopularityNumber = 0,
                    SmallCoverImage = File.ReadAllBytes(small),
                    NormalCoverImage = File.ReadAllBytes(large)
                });
            }


            small = Path.Combine(imageDirectory, "szent_peter_esernyoje_thumb.jpg");
            large = Path.Combine(imageDirectory, "szent_peter_esernyoje.jpg");

            if (File.Exists(small) && File.Exists(large))
            {
                books.Add(new Book
                {
                    Title = "Szent Péter esernyője",
                    Writer = "Mikszáth Kálmán",
                    Year = 2012,
                    ISBNNumber = "9786066461580",
                    PopularityNumber = 0,
                    SmallCoverImage = File.ReadAllBytes(small),
                    NormalCoverImage = File.ReadAllBytes(large)
                });
            }


            small = Path.Combine(imageDirectory, "szoftverfejlesztes_okosan_pythonnal_thumb.jpg");
            large = Path.Combine(imageDirectory, "szoftverfejlesztes_okosan_pythonnal.jpg");

            if (File.Exists(small) && File.Exists(large))
            {
                books.Add(new Book
                {
                    Title = "Szoftverfejlesztés okosan Pythonnal",
                    Writer = "Dr. Guta Gábor",
                    Year = 2020,
                    ISBNNumber = "9786155186745",
                    PopularityNumber = 0,
                    SmallCoverImage = File.ReadAllBytes(small),
                    NormalCoverImage = File.ReadAllBytes(large)
                });
            }

            small = Path.Combine(imageDirectory, "toldi_thumb.jpg");
            large = Path.Combine(imageDirectory, "toldi.jpg");

            if (File.Exists(small) && File.Exists(large))
            {
                books.Add(new Book
                {
                    Title = "Toldi",
                    Writer = "Arany János",
                    Year = 2021,
                    ISBNNumber = "9786155641336",
                    PopularityNumber = 0,
                    SmallCoverImage = File.ReadAllBytes(small),
                    NormalCoverImage = File.ReadAllBytes(large)
                });
            }

            small = Path.Combine(imageDirectory, "vallomasok_erdelyrol_thumb.jpg");
            large = Path.Combine(imageDirectory, "vallomasok_erdelyrol.jpg");

            if (File.Exists(small) && File.Exists(large))
            {
                books.Add(new Book
                {
                    Title = "Vallomások Erdélyről",
                    Writer = "Kós Károly",
                    Year = 2022,
                    ISBNNumber = "9786156016775",
                    PopularityNumber = 0,
                    SmallCoverImage = File.ReadAllBytes(small),
                    NormalCoverImage = File.ReadAllBytes(large)
                });
            }

            small = Path.Combine(imageDirectory, "viragszal_thumb.jpg");
            large = Path.Combine(imageDirectory, "viragszal.jpg");

            if (File.Exists(small) && File.Exists(large))
            {
                books.Add(new Book
                {
                    Title = "Virágszál",
                    Writer = "Fábián Janka",
                    Year = 2019,
                    ISBNNumber = "9789634336020",
                    PopularityNumber = 0,
                    SmallCoverImage = File.ReadAllBytes(small),
                    NormalCoverImage = File.ReadAllBytes(large)
                });
            }

            small = Path.Combine(imageDirectory, "webszerkesztes_konnyeden_thumb.jpg");
            large = Path.Combine(imageDirectory, "webszerkesztes_konnyeden.jpg");

            if (File.Exists(small) && File.Exists(large))
            {
                books.Add(new Book
                {
                    Title = "Webszerkesztés könnyedén",
                    Writer = "Holczer József",
                    Year = 2017,
                    ISBNNumber = "9786155012044",
                    PopularityNumber = 0,
                    SmallCoverImage = File.ReadAllBytes(small),
                    NormalCoverImage = File.ReadAllBytes(large)
                });
            }

            small = Path.Combine(imageDirectory, "zarah_alma_thumb.jpg");
            large = Path.Combine(imageDirectory, "zarah_alma.jpg");

            if (File.Exists(small) && File.Exists(large))
            {
                books.Add(new Book
                {
                    Title = "Zarah álma",
                    Writer = "Náray Tamás",
                    Year = 2022,
                    ISBNNumber = "9789634339250",
                    PopularityNumber = 0,
                    SmallCoverImage = File.ReadAllBytes(small),
                    NormalCoverImage = File.ReadAllBytes(large)
                });
            }

            small = Path.Combine(imageDirectory, "zrinyi_miklos_emlekkonyv_thumb.jpg");
            large = Path.Combine(imageDirectory, "zrinyi_miklos_emlekkonyv.jpg");

            if (File.Exists(small) && File.Exists(large))
            {
                books.Add(new Book
                {
                    Title = "Zrínyi Miklós emlékkönyv",
                    Writer = "Pálffy Géza",
                    Year = 2022,
                    ISBNNumber = "9786156428004",
                    PopularityNumber = 0,
                    SmallCoverImage = File.ReadAllBytes(small),
                    NormalCoverImage = File.ReadAllBytes(large)
                });
            }

            foreach (Book b in books)
            {
                context.Books.Add(b);
            }

            context.SaveChanges();

            #endregion Books

            #region Volumes

            var volumes = new List<Volume>();
            foreach (Book book in context.Books)
            {
                for (int i = 1; i <= 5; ++i)
                {
                    volumes.Add(new Volume
                    {
                        BookId = book.Id,
                    });
                }
            }

            foreach (Volume volume in volumes)
            {
                context.Volumes.Add(volume);
            }

            context.SaveChanges();

            #endregion

            #region Rents

            var rents = new List<Rent>
            {
                new Rent
                {
                    Start = DateTime.Today,
                    End = DateTime.Today + TimeSpan.FromDays(3),
                    VolumeId = 1,
                    VisitorId = 1,
                    IsActive = false
                },
                new Rent
                {
                    Start = DateTime.Today + TimeSpan.FromDays(4),
                    End = DateTime.Today + TimeSpan.FromDays(5),
                    VolumeId = 1,
                    VisitorId = 2,
                    IsActive = false
                },
                new Rent
                {
                    Start = DateTime.Today + TimeSpan.FromDays(10),
                    End = DateTime.Today + TimeSpan.FromDays(15),
                    VolumeId = 6,
                    VisitorId = 1,
                    IsActive = false
                }
            };

            foreach (Rent rent in rents)
            {
                context.Rents.Add(rent);
            }

            context.SaveChanges();

            #endregion
        }
    }
}
