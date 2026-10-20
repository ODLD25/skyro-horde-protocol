### Tabuľka 1

| Stav       | Čo vlna robí                                                                     |
| ---------- | -------------------------------------------------------------------------------- |
| `Run`      | Vlna beží. Hráč sa hýbe a strieľa, strela letí, nepriateľ ide                    |
| `Pause`    | Vlna stojí na mieste. Ďalšie `Pause` pokračuje v tej istej vlne, nie od začiatku |
| `GameOver` | Vlna sa skončila smrťou. Stojí, kým nepríde `Retry`                              |

### Tabuľka 2

| Teraz               | Udalosť          | Potom                                 |
| ------------------- | ---------------- | ------------------------------------- |
| `Run`               | `Pause`          | `Pause`                               |
| `Pause`             | `Pause`          | `Run`, tá istá vlna, tie isté pozície |
| `Run`               | dotyk nepriateľa | `GameOver`, hráč ostáva na scéne      |
| `Pause`             | dotyk nepriateľa | `Pause`, smrť sa neráta               |
| `GameOver`          | `Pause`          | `GameOver`                            |
| `GameOver`          | `Retry`          | vlna od začiatku, stav `Run`          |
| `Run` alebo `Pause` | `Retry`          | stav sa nemení                        |

### Tabuľka 3

| Príkaz  | `Run`             | `Pause`         | `GameOver`         |
| ------- | ----------------- | --------------- | ------------------ |
| `Move`  | vykoná sa         | zahodí sa       | zahodí sa          |
| `Fire`  | vykoná sa         | zahodí sa       | zahodí sa          |
| `Pause` | prejde do `Pause` | prejde do `Run` | zahodí sa          |
| `Retry` | zahodí sa         | zahodí sa       | načíta Arénu znova |

Cvicenie 1

1. Stav hry je paused a enemaci stoja na mieste.
2. Stav hry je Pause
3. Nic
4. brat inputy