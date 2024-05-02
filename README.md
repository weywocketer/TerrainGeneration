Proceduralna generacja terenu 2D, oparta o uproszczoną implementację algorytmu Wave Function Collapse.

Możliwości konfiguracji:
- Do GameObject'u TerrainGenerator dołączony jest komponent WFC, w którym można wprowadzić własne wymiary mapy (jak również ustawić opóźnienie wykonywania się algorytmu, aby wygodniej obserwować proces generacji terenu).
- Dla poszczególnych Prefab'ów (w folderze Prefabs) możemy modyfikować parametr Weight (w komponencie WfcTile). Powinna przyjmować wartości naturalne, im wyższa waga, tym częściej będzie umieszczany dany kafelek.
