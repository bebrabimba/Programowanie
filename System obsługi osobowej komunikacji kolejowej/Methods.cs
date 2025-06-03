using System;
using System.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace System_obsługi_osobowej_komunikacji_kolejowej
{
    public partial class fon
    {

        #region Metody


        private void LoadBiletyData(DataGridView dgv)
        {
            // 1) Odczyt connection string z App.config:
            string connString = ConfigurationManager
                .ConnectionStrings["SystemOOKK_ConnectionString"]
                .ConnectionString;

            // 2) Zapytanie SQL łączące tabele:
            string query = @"
        SELECT 
            B.IDBiletu,
            B.IDPociągu,
            P.Imię       AS ImięPasażera,
            P.Nazwisko   AS NazwiskoPasażera,
            B.DataZakupu,
            B.CenaBiletu,
            B.Wykorzystany,
            S.NazwaPrzystanku AS PrzystanekStartowy,
            E.NazwaPrzystanku AS PrzystanekKoncowy
        FROM Bilety AS B
        INNER JOIN Pasażerowie AS P
            ON B.IDBiletu = P.IDBiletu
        INNER JOIN Przystanki AS S
            ON B.IDStart = S.IDPrzystanku
        INNER JOIN Przystanki AS E
            ON B.IDStop  = E.IDPrzystanku
        ORDER BY B.IDBiletu;
    ";

            DataTable dt = new DataTable();
            try
            {
                // 3) Pobranie danych:
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        adapter.Fill(dt);
                    }
                }

                // 4) Zwiąż DataTable z DataGridView:
                dgv.DataSource = dt;

                // 5) Ustawiamy nagłówki kolumn (zamiana tekstu SQL → etykieta wyświetlana),
                //    włączamy zawijanie w nagłówkach:
                dgv.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;

                int columnCount = dgv.Columns.Count;
                // Tablica na pomiar: dla każdej kolumny
                //   headerFullWidth[col], headerMaxWordWidth[col],
                //   maxCellFullWidth[col], maxCellMaxWordWidth[col]
                int[] headerFullWidth = new int[columnCount];
                int[] headerMaxWordWidth = new int[columnCount];
                int[] maxCellFullWidth = new int[columnCount];
                int[] maxCellMaxWordWidth = new int[columnCount];

                // Potrzebujemy Graphics do pomiaru:
                using (Graphics g = dgv.CreateGraphics())
                {
                    for (int col = 0; col < columnCount; col++)
                    {
                        // 6.a) Pomiar nagłówka:
                        string rawHeader = dgv.Columns[col].HeaderText;
                        string displayedHeader = rawHeader
                            .Replace("IDBiletu", "ID Biletu")
                            .Replace("IDPociągu", "№ Pociągu")
                            .Replace("ImięPasażera", "Imię")
                            .Replace("NazwiskoPasażera", "Nazwisko")
                            .Replace("DataZakupu", "Data zakupu")
                            .Replace("CenaBiletu", "Cena")
                            .Replace("Wykorzystany", "Wykorzystany")
                            .Replace("PrzystanekStartowy", "Przystanek startowy")
                            .Replace("PrzystanekKoncowy", "Przystanek końcowy");

                        dgv.Columns[col].HeaderText = displayedHeader;

                        // 6.a.i) Pełna szerokość nagłówka (jeden wiersz):
                        Size fullHeaderSize = TextRenderer.MeasureText(
                            g,
                            displayedHeader,
                            dgv.ColumnHeadersDefaultCellStyle.Font);
                        headerFullWidth[col] = fullHeaderSize.Width + 20; // + padding

                        // 6.a.ii) Najdłuższe słowo w nagłówku:
                        int maxHeaderWord = 0;
                        foreach (var w in displayedHeader.Split(' '))
                        {
                            int wWidth = TextRenderer.MeasureText(
                                g,
                                w,
                                dgv.ColumnHeadersDefaultCellStyle.Font).Width + 10;
                            if (wWidth > maxHeaderWord)
                                maxHeaderWord = wWidth;
                        }
                        headerMaxWordWidth[col] = maxHeaderWord;

                        // 6.a.iii) Jeśli nagłówek to więcej niż jedno słowo, zwiększamy wysokość wiersza nagłówka:
                        if (displayedHeader.Contains(" "))
                        {
                            int twoLineHeight = TextRenderer.MeasureText(
                                g,
                                displayedHeader,
                                dgv.ColumnHeadersDefaultCellStyle.Font).Height * 3;
                            if (dgv.ColumnHeadersHeight < twoLineHeight)
                                dgv.ColumnHeadersHeight = twoLineHeight;
                        }

                        // 6.b) Teraz mierzymy komórki row po row w tej kolumnie:
                        int maxCellFull = 0;
                        int maxCellWord = 0;
                        foreach (DataGridViewRow row in dgv.Rows)
                        {
                            if (row.IsNewRow) continue;
                            object cellObj = row.Cells[col].Value;
                            if (cellObj == null) continue;

                            string cellText = cellObj.ToString();

                            // 6.b.i) Pomiar całego tekstu:
                            int cellFull = TextRenderer.MeasureText(
                                g,
                                cellText,
                                dgv.RowsDefaultCellStyle.Font).Width + 10;
                            if (cellFull > maxCellFull)
                                maxCellFull = cellFull;

                            // 6.b.ii) Pomiar najdłuższego słowa:
                            foreach (var w in cellText.Split(' '))
                            {
                                int wWidth = TextRenderer.MeasureText(
                                    g,
                                    w,
                                    dgv.RowsDefaultCellStyle.Font).Width + 10;
                                if (wWidth > maxCellWord)
                                    maxCellWord = wWidth;
                            }
                        }

                        maxCellFullWidth[col] = maxCellFull;
                        maxCellMaxWordWidth[col] = maxCellWord;
                    }
                }



                int[] desiredWidth = new int[columnCount];
                for (int i = 0; i < columnCount; i++)
                {
                    desiredWidth[i] = Math.Max(headerFullWidth[i], maxCellFullWidth[i]);
                }

                int sumDesired = desiredWidth.Sum();

                if (sumDesired <= MaxTotalWidth)
                {
                    // Wystarczy przypisać pożądane szerokości:
                    for (int i = 0; i < columnCount; i++)
                    {
                        dgv.Columns[i].Width = desiredWidth[i];
                        // Domyślnie bez zawijania:
                        dgv.Columns[i].DefaultCellStyle.WrapMode = DataGridViewTriState.False;
                        dgv.Columns[i].HeaderCell.Style.WrapMode = DataGridViewTriState.True;
                    }
                }
                else
                {
                    // 7.a) Oblicz średnią szerokość:
                    int avgWidth = MaxTotalWidth / columnCount;

                    int[] finalWidth = new int[columnCount];
                    bool[] wrapColumn = new bool[columnCount];

                    for (int i = 0; i < columnCount; i++)
                    {
                        // Jeśli desired[i] > avgWidth, ale zarówno headerMaxWordWidth[i] <= avgWidth 
                        // jak i maxCellMaxWordWidth[i] <= avgWidth,
                        // to zawijamy (wrap = true) i ustawiamy szerokość jako max(
                        //    headerMaxWordWidth[i], maxCellMaxWordWidth[i]).
                        int maxWordCandidate = Math.Max(headerMaxWordWidth[i], maxCellMaxWordWidth[i]);
                        if (desiredWidth[i] > avgWidth && maxWordCandidate <= avgWidth)
                        {
                            wrapColumn[i] = true;
                            finalWidth[i] = maxWordCandidate;
                        }
                        else
                        {
                            wrapColumn[i] = false;
                            finalWidth[i] = desiredWidth[i];
                        }
                    }

                    int sumFinal = finalWidth.Sum();
                    if (sumFinal <= MaxTotalWidth)
                    {
                        // Ustaw szerokości final i odpowiedni wrap:
                        for (int i = 0; i < columnCount; i++)
                        {
                            dgv.Columns[i].Width = finalWidth[i];
                            dgv.Columns[i].DefaultCellStyle.WrapMode = wrapColumn[i]
                                ? DataGridViewTriState.True
                                : DataGridViewTriState.False;
                            dgv.Columns[i].HeaderCell.Style.WrapMode = DataGridViewTriState.True;
                        }
                    }
                    else
                    {
                        // 7.b) Nadal za szeroko → rozdziel proporcjonalnie do finalWidth:
                        for (int i = 0; i < columnCount; i++)
                        {
                            double proportion = (double)finalWidth[i] / sumFinal;
                            int w = (int)Math.Floor(proportion * MaxTotalWidth);
                            dgv.Columns[i].Width = Math.Max(w, 50);
                            dgv.Columns[i].DefaultCellStyle.WrapMode = wrapColumn[i]
                                ? DataGridViewTriState.True
                                : DataGridViewTriState.False;
                            dgv.Columns[i].HeaderCell.Style.WrapMode = DataGridViewTriState.True;
                        }
                        // Korekta resztkowych pikseli:
                        int assignedSum = dgv.Columns.Cast<DataGridViewColumn>().Sum(c => c.Width);
                        int diff = MaxTotalWidth - assignedSum;
                        if (diff > 0 && columnCount > 0)
                            dgv.Columns[0].Width += diff;
                    }
                }

                // 8) Dodatkowe wyrównanie:
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;   // nagłówki do lewej
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;     // komórki do lewej
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Nie udało się wczytać danych listy biletów:\n" + ex.Message,
                    "Błąd ładowania danych",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void LoadPociagiData(DataGridView dgv)
        {
            // 1) Pobranie connection string z App.config:
            string connString = ConfigurationManager
                .ConnectionStrings["SystemOOKK_ConnectionString"]
                .ConnectionString;

            // 2) Zapytanie SQL pobierające IDPociągu, Maszynistę, Konduktora i Kierunek:
            string query = @"
        SELECT 
            P.IDPociągu,
            M.Imię    + ' ' + M.Nazwisko    AS Maszynista,
            K.Imię    + ' ' + K.Nazwisko    AS Konduktor,
            CASE P.Kierunek 
                WHEN 1 THEN 'Andrzejów - Emilianów'
                ELSE 'Emilianów - Andrzejów'
            END                               AS Kierunek
        FROM Pociągi AS P
        INNER JOIN Maszyniści   AS M ON P.IDMaszynisty   = M.IDMaszynisty
        INNER JOIN Konduktorzy  AS K ON P.IDKonduktora   = K.IDKonduktora
        ORDER BY P.IDPociągu;
    ";

            // 3) Wczytanie wyników do DataTable:
            var dt = new DataTable();
            using (var conn = new SqlConnection(connString))
            using (var adapter = new SqlDataAdapter(query, conn))
            {
                adapter.Fill(dt);
            }

            // 4) Przypisanie DataTable jako DataSource dla dgv:
            dgv.DataSource = dt;

            // 5) Wyłączenie sortowania i wyrównanie nagłówków/komórek do lewej:
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
            dgv.ClearSelection();

            // 6) Ustawienie, by kolumny wypełniały całą dostępną szerokość (≈940px):
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void CmbStartPrzystanek_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Reset:
            lblCena.Text = "Cena biletu: 0 zł";
            lblArrivalTime.Text = "-";
            cmbStartGodzina.Items.Clear();
            cmbStartGodzina.Enabled = false;

            cmbStopPrzystanek.Items.Clear();
            cmbStopPrzystanek.Enabled = false;

            // Jeśli nic nie wybrano:
            if (cmbStartPrzystanek.SelectedItem == null)
                return;

            string wybranyStart = cmbStartPrzystanek.SelectedItem as string;

            // Wypełniamy listę stacji końcowych (wszystkie poza wybranym startem):
            foreach (DataRow row in dtPrzystanki_All.Rows)
            {
                string nazwa = row.Field<string>("NazwaPrzystanku");
                if (nazwa != wybranyStart && !cmbStopPrzystanek.Items.Contains(nazwa))
                {
                    cmbStopPrzystanek.Items.Add(nazwa);
                }
            }
            cmbStopPrzystanek.Enabled = true;
        }

        private void CmbStopPrzystanek_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 1) Reset ceny i godziny przyjazdu:
            lblCena.Text = "Cena biletu: 0 zł";
            lblArrivalTime.Text = "-";
            cmbStartGodzina.Items.Clear();
            cmbStartGodzina.Enabled = false;

            // 2) Jeżeli nie wybrano startu lub stopu, nic nie róbmy:
            if (cmbStartPrzystanek.SelectedItem == null || cmbStopPrzystanek.SelectedItem == null)
                return;

            string wybranyStart = cmbStartPrzystanek.SelectedItem as string;
            string wybranyStop = cmbStopPrzystanek.SelectedItem as string;

            // 3) Zbudujmy listę odjazdów z wybranego przystanku startowego (bez 00:00:00):
            var listaStartów = dtPrzystanki_All.AsEnumerable()
                .Where(r =>
                    r.Field<string>("NazwaPrzystanku") == wybranyStart &&
                    r.Field<TimeSpan>("Odjazdy") != TimeSpan.Zero
                )
                .Select(r =>
                {
                    short raw = r.Field<short>("IDPrzystanku");
                    string sRaw = raw.ToString();                    // np. "7122"
                    int cycle = int.Parse(sRaw.Substring(0, 1));   // np. 7
                    int trainNum = int.Parse(sRaw.Substring(1, 2));   // np. 12
                    TimeSpan dep = r.Field<TimeSpan>("Odjazdy");      // godzina odjazdu
                    return new { Cycle = cycle, TrainNum = trainNum, Departure = dep };
                })
                .ToList();

            // 4) Zbudujmy listę przyjazdów na wybrany przystanek końcowy (bez 00:00:00):
            var listaStopów = dtPrzystanki_All.AsEnumerable()
                .Where(r =>
                    r.Field<string>("NazwaPrzystanku") == wybranyStop &&
                    r.Field<TimeSpan>("Przyjazdy") != TimeSpan.Zero
                )
                .Select(r =>
                {
                    short raw = r.Field<short>("IDPrzystanku");
                    string sRaw = raw.ToString();                    // np. "7122"
                    int cycle = int.Parse(sRaw.Substring(0, 1));   // np. 7
                    int trainNum = int.Parse(sRaw.Substring(1, 2));   // np. 12
                    TimeSpan arr = r.Field<TimeSpan>("Przyjazdy");    // godzina przyjazdu
                    return new { Cycle = cycle, TrainNum = trainNum, Arrival = arr };
                })
                .ToList();

            // 5) Dla każdego odjazdu sprawdźmy: 
            //    spośród przyjazdów o tym samym (Cycle, TrainNum) wybierzmy tę, której Arrival > Departure,
            //    a potem z nich najmniejszą (OrderBy(x => x.Arrival).FirstOrDefault()).
            foreach (var s in listaStartów)
            {
                var matchStop = listaStopów
                    .Where(x => x.Cycle == s.Cycle
                             && x.TrainNum == s.TrainNum
                             && x.Arrival > s.Departure)
                    .OrderBy(x => x.Arrival)
                    .FirstOrDefault();

                if (matchStop != null)
                {
                    cmbStartGodzina.Items.Add(s.Departure.ToString(@"hh\:mm\:ss"));
                }
            }

            // 6) Odblokowujemy combobox, jeśli mamy chociaż jedną godzinę odjazdu:
            cmbStartGodzina.Enabled = (cmbStartGodzina.Items.Count > 0);
            cmbStartGodzina.SelectedIndex = -1;
        }

        private void CmbGodz_SelectedIndexChanged(object sender, EventArgs e)
        {
            RecalculatePrice();
        }

        private void ChkUlga_CheckedChanged(object sender, EventArgs e)
        {
            RecalculatePrice();
        }

        private void RecalculatePrice()
        {
            // Jeśli nie wybrano wszystkiego, cena = 0:
            if (cmbStartPrzystanek.SelectedItem == null
                || cmbStopPrzystanek.SelectedItem == null
                || cmbStartGodzina.SelectedItem == null)
            {
                lblCena.Text = "Cena biletu: 0 zł";
                return;
            }

            string wybranyStart = cmbStartPrzystanek.SelectedItem as string;
            string wybranyStop = cmbStopPrzystanek.SelectedItem as string;
            string godzOdjazdu = cmbStartGodzina.SelectedItem as string;
            TimeSpan tsOdjazdu = TimeSpan.Parse(godzOdjazdu);

            // Wyciągnijmy IDStart i IDStop tak samo jak w CmbStartGodzina_SelectedIndexChanged:
            var rowStart = dtPrzystanki_All.AsEnumerable()
                .First(r =>
                    r.Field<string>("NazwaPrzystanku") == wybranyStart &&
                    r.Field<TimeSpan>("Odjazdy") == tsOdjazdu
                );
            short idStart = rowStart.Field<short>("IDPrzystanku");

            // Do znalezienia IDStop ponownie tniemy według trainNum i nazwy stacji:
            string sIdStart = idStart.ToString();       // np. "7122"
            int trainNum = int.Parse(sIdStart.Substring(1, 2));

            var rowStop = dtPrzystanki_All.AsEnumerable()
                .First(r =>
                {
                    short raw = r.Field<short>("IDPrzystanku");
                    string sRaw = raw.ToString();
                    int tn = int.Parse(sRaw.Substring(1, 2));
                    return tn == trainNum && r.Field<string>("NazwaPrzystanku") == wybranyStop;
                });
            short idStop = rowStop.Field<short>("IDPrzystanku");

            int stopNumStart = idStart % 10;
            int stopNumStop = idStop % 10;
            int distance = Math.Abs(stopNumStart - stopNumStop);

            int rawPrice = distance * 7;
            if (chkUlga.Checked)
                rawPrice /= 2;

            lblCena.Text = $"Cena biletu: {rawPrice} zł";
        }

        private void CmbStartGodzina_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Reset ceny i godziny przyjazdu:
            lblCena.Text = "Cena biletu: 0 zł";
            lblArrivalTime.Text = "-";

            // Jeżeli brakuje startu, stopu lub wybranej godziny, przerywamy:
            if (cmbStartPrzystanek.SelectedItem == null
                || cmbStopPrzystanek.SelectedItem == null
                || cmbStartGodzina.SelectedItem == null)
            {
                return;
            }

            string wybranyStart = cmbStartPrzystanek.SelectedItem as string;
            string wybranyStop = cmbStopPrzystanek.SelectedItem as string;
            string godzOdjazdu = cmbStartGodzina.SelectedItem as string; // np. "08:30:00"
            TimeSpan tsOdjazdu = TimeSpan.Parse(godzOdjazdu);

            // 1) Znajdźmy wiersz odpowiadający temu odjazdowi:
            var rowStart = dtPrzystanki_All.AsEnumerable()
                .First(r =>
                    r.Field<string>("NazwaPrzystanku") == wybranyStart &&
                    r.Field<TimeSpan>("Odjazdy") == tsOdjazdu
                );
            short idStart = rowStart.Field<short>("IDPrzystanku");      // np. 7122
            string sIdStart = idStart.ToString();                       // "7122"
            int cycle = int.Parse(sIdStart.Substring(0, 1));         // 7
            int trainNum = int.Parse(sIdStart.Substring(1, 2));         // 12

            // 2) Wyszukujemy w dtPrzystanki_All wiersz, który ma ten sam (cycle, trainNum) i nazwa == wybranyStop:
            var rowStop = dtPrzystanki_All.AsEnumerable()
                .First(r =>
                {
                    short raw = r.Field<short>("IDPrzystanku");
                    string sRaw = raw.ToString();
                    int c = int.Parse(sRaw.Substring(0, 1));
                    int t = int.Parse(sRaw.Substring(1, 2));
                    return c == cycle
                        && t == trainNum
                        && r.Field<string>("NazwaPrzystanku") == wybranyStop;
                });

            TimeSpan tsPrzyjazdu = rowStop.Field<TimeSpan>("Przyjazdy");
            lblArrivalTime.Text = (tsPrzyjazdu == TimeSpan.Zero)
                ? "-"
                : tsPrzyjazdu.ToString(@"hh\:mm\:ss");

            // 3) Przeliczmy jeszcze cenę biletu:
            RecalculatePrice();
        }

        private void BtnKupBilet_Click(object sender, EventArgs e)
        {
            // --- 1) WALIDACJA IMIENIA / NAZWISKA ----------------------------------
            lblErrorImie.Text = "";
            lblErrorNazwisko.Text = "";

            string imie = txtImie.Text.Trim();
            string nazwisko = txtNazwisko.Text.Trim();

            // Dozwolone: litery A-Z, a-z oraz polskie znaki: ÓóĄąĘęŚśŁłŻżŹźĆćŃń i ewent. spacja / dywiz
            var regex = new System.Text.RegularExpressions.Regex(@"^[A-Za-zÓóĄąĘęŚśŁłŻżŹźĆćŃń\- ]+$");

            bool poprawneImie = regex.IsMatch(imie);
            bool poprawneNazwisko = regex.IsMatch(nazwisko);

            if (!poprawneImie)
            {
                lblErrorImie.Text = "Podaj poprawne imię";
            }
            if (!poprawneNazwisko)
            {
                lblErrorNazwisko.Text = "Podaj poprawne nazwisko";
            }
            if (!poprawneImie || !poprawneNazwisko)
            {
                return;
            }

            // --- 2) SPRAWDZENIE, CZY WYBRANO PRZYSTANKI I GODZINĘ ----------------
            if (cmbStartPrzystanek.SelectedItem == null ||
                cmbStopPrzystanek.SelectedItem == null ||
                cmbStartGodzina.SelectedItem == null)
            {
                MessageBox.Show(
                    "Proszę wybrać przystanek początkowy, końcowy oraz godzinę odjazdu.",
                    "Brak danych", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string connString = ConfigurationManager
                    .ConnectionStrings["SystemOOKK_ConnectionString"]
                    .ConnectionString;

                using (var conn = new SqlConnection(connString))
                {
                    conn.Open();

                    // --- 3) USTALAMY NOWY IDBiletu: MAX(IDBiletu)+1 ------------------------
                    int nextIdBiletu = 1;
                    using (var cmdMax = new SqlCommand("SELECT ISNULL(MAX(IDBiletu), 0) + 1 FROM Bilety", conn))
                    {
                        nextIdBiletu = (int)cmdMax.ExecuteScalar();
                    }

                    // --- 4) WYCIĄGAMY WSZYSTKIE DANE POTRZEBNE PONIŻEJ ----------------------
                    string wybranyStart = cmbStartPrzystanek.SelectedItem as string;
                    string wybranyStop = cmbStopPrzystanek.SelectedItem as string;
                    string godzOdjazdu = cmbStartGodzina.SelectedItem as string;
                    TimeSpan tsOdjazdu = TimeSpan.Parse(godzOdjazdu);

                    // 4a) Z tabeli dtPrzystanki_All znajdujemy wiersz, w którym
                    //      • NazwaPrzystanku == wybranyStart
                    //      • Odjazdy == tsOdjazdu
                    var rowStart = dtPrzystanki_All.AsEnumerable()
                        .First(r =>
                            r.Field<string>("NazwaPrzystanku") == wybranyStart &&
                            r.Field<TimeSpan>("Odjazdy") == tsOdjazdu
                        );
                    short idStart = rowStart.Field<short>("IDPrzystanku");

                    // 4b) Obliczamy IDPociągu: pobieramy 2 środkowe cyfry z IDPrzystanku:
                    //      np. raw="7123" → środkowe "12" → int 12.
                    string sIdStart = idStart.ToString();            // np. "7123"
                    int trainNum = int.Parse(sIdStart.Substring(1, 2)); // np. "12" → 12

                    // 4c) Znajdźmy IDStop — w tej samej tabeli dtPrzystanki_All:
                    //      to będzie wiersz z tej samej trasy (tj. tych samych 2 środkowych cyfr)
                    //      i z nazwą stacji = wybranyStop.
                    var rowStop = dtPrzystanki_All.AsEnumerable()
                        .First(r =>
                        {
                            short raw = r.Field<short>("IDPrzystanku");
                            string sRaw = raw.ToString();
                            int tn = int.Parse(sRaw.Substring(1, 2));
                            return tn == trainNum && r.Field<string>("NazwaPrzystanku") == wybranyStop;
                        });
                    short idStop = rowStop.Field<short>("IDPrzystanku");

                    // 4d) Cena biletu — z etykiety lblCena w formacie "Cena biletu: X zł"
                    //      Usuńmy przedrostek i dopisek " zł", zostawmy tylko liczbę:
                    string cenaTekst = lblCena.Text
                        .Replace("Cena biletu:", "")
                        .Replace("zł", "")
                        .Trim();
                    int cenaBiletu = 0;
                    int.TryParse(cenaTekst, out cenaBiletu);

                    // 4e) Ulga? (bit 1 lub 0)
                    int ulgaBit = chkUlga.Checked ? 1 : 0;

                    // --- 5) INSERT DO TABELI PASAŻEROWIE (NAJPIERW!) ----------------------
                    //      (IDBiletu, Imię, Nazwisko, Ulga)
                    string insertPasazer = @"
                INSERT INTO Pasażerowie
                    (IDBiletu, Imię, Nazwisko, Ulga)
                VALUES
                    (@IDBiletu, @Imie, @Nazwisko, @Ulga);
            ";
                    using (var cmdInsPasazer = new SqlCommand(insertPasazer, conn))
                    {
                        cmdInsPasazer.Parameters.AddWithValue("@IDBiletu", nextIdBiletu);
                        cmdInsPasazer.Parameters.AddWithValue("@Imie", imie);
                        cmdInsPasazer.Parameters.AddWithValue("@Nazwisko", nazwisko);
                        cmdInsPasazer.Parameters.AddWithValue("@Ulga", ulgaBit);

                        cmdInsPasazer.ExecuteNonQuery();
                    }

                    // --- 6) INSERT DO TABELI BILETY (PO DRUGIE!) --------------------------
                    //      (IDBiletu, IDPociągu, IDStart, IDStop, DataZakupu, CenaBiletu, Wykorzystany)
                    string insertBilet = @"
                INSERT INTO Bilety
                    (IDBiletu, IDPociągu, IDStart, IDStop, DataZakupu, CenaBiletu, Wykorzystany)
                VALUES
                    (@IDBiletu, @IDPociągu, @IDStart, @IDStop, @DataZakupu, @CenaBiletu, @Wykorzystany);
            ";
                    using (var cmdInsBilet = new SqlCommand(insertBilet, conn))
                    {
                        cmdInsBilet.Parameters.AddWithValue("@IDBiletu", nextIdBiletu);
                        cmdInsBilet.Parameters.AddWithValue("@IDPociągu", trainNum);
                        cmdInsBilet.Parameters.AddWithValue("@IDStart", idStart);
                        cmdInsBilet.Parameters.AddWithValue("@IDStop", idStop);
                        cmdInsBilet.Parameters.AddWithValue("@DataZakupu", DateTime.Now);
                        cmdInsBilet.Parameters.AddWithValue("@CenaBiletu", cenaBiletu);
                        cmdInsBilet.Parameters.AddWithValue("@Wykorzystany", 0);

                        cmdInsBilet.ExecuteNonQuery();
                    }

                    conn.Close();
                }

                // --- 7) PO ZAKUPIE — komunikat i wyczyszczenie formularza -------------
                MessageBox.Show(
                    "Bilet został poprawnie zakupiony.",
                    "Sukces",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                txtImie.Clear();
                txtNazwisko.Clear();
                cmbStartPrzystanek.SelectedIndex = -1;
                cmbStopPrzystanek.SelectedIndex = -1;
                cmbStartGodzina.Items.Clear();
                cmbStartGodzina.Enabled = false;
                lblArrivalTime.Text = "-";
                chkUlga.Checked = false;
                lblCena.Text = "Cena biletu: 0 zł";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Wystąpił błąd podczas zakupu biletu:\n{ex.Message}",
                    "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion

    }
}
