# ksiazkoczytacz

Aplikacja pomaga mi rozwijać umiejętności pisania w języku C#. W chwili obecnej nie występuje na githubie jako open source,
ponieważ w rozbudowanej wersji chciałbym wykorzystać ją do celów komercyjnych.
W swoim głównym założeniu ma ona pomagać użytkownikowi uczyć się języka angielskiego poprzez czytanie książek.

Na samą aplikację składają się: 
- wyciąganie słówek z książki (np .pdf) które nie pokrywają się z bazą nauczonych słówek (wykluczając również odmiany słówek z bazy: -ing, -ed, -{podwojenie sylaby}ed),
- tłumaczenie ich lub w najnowszej wersji podawanie opisów tych słówek ze słownika języka angielskiego (stworzyłem do tego dwa słowniki w plikach .CSV oraz .txt),
- GUI,
- Naukę słówek poprzez odpytywanie słówek i czytanie przez komputer na głos w języku angielskim (na ten moment zablokowane dla C# ponieważ dla C# na windows działa wolniej
niż w pierwotnej wersji odpytywacza)

Przy okazji jej pisania stworzyłem również:
- wyciąganie słówek ze strony internetowej dla różnych poziomów języka angielskiego (aplikacja też będzie ich uczyć). W ustawieniach można zaznaczyć nasz poziom języka
i z góry wykluczyć część słówek,
- Prosty tłumacz słówek z wykorzystaniem Azure.
- Korzystanie z bazy danych przy pomocy Entity Framework (w celach edukacyjnych, obecnie szybsze wydaje mi się korzystanie z prostych posortowanych plików .csv/.txt).
- Proste testy jednostkowe w roli przypomnienia
