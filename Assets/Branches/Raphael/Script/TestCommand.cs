using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestCommand : MonoBehaviour
{
//     public RoundOver round;
//     public TMP_InputField input;
//     public GameObject canvas;
//     public GameObject amountIN;
//     [SerializeField] TextUI textUI;
//     [SerializeField] TextManager textM;
//     [SerializeField] SFXManager sfx;
//     public int number;
//     int choice;
//     // Start is called before the first frame update
// 
// 
// 
//     private void Start()
//     {
//         Initialize();
//     }
// 
//     private void Update()
//     {
//         Refresh();
//     }
//     public void Initialize()
//     {
// 
//     }
//     public void Refresh()
//     {
//         if (Input.GetKey(KeyCode.DownArrow))
//         {
//             canvas.SetActive(true);
//             amountIN.SetActive(false);
//         }
//         else if (Input.GetKey(KeyCode.UpArrow))
//         {
//             canvas.SetActive(false);
//         }
// 
//     }
//     public void CheckCheat()
//     {
// 
//         if (input.text == "win")
//         {
//             round.ShowVictory();
//         }
//         else if (input.text == "lose")
//         {
//             round.ShowDefeat();
//         }
//         else if (input.text == "hide")
//         {
//             round.HideUI();
//         }
//         else if (input.text == "reset")
//         {
//             textUI.Reset();
//         }
//         else if (input.text == "turn on")
//         {
//             textM.TurningOn();
//         }
//         else if (input.text == "turn off")
//         {
//             textM.TurningOff();
//         }
//         else if (input.text == "add score")
//         {
//             amountIN.SetActive(true);
//             choice = 1;
//         }
//         else if (input.text == "add cash")
//         {
//             amountIN.SetActive(true);
//             choice = 2;
//         }
//         else if (input.text == "add headshot")
//         {
//             amountIN.SetActive(true);
//             choice = 3;
//         }
//         else if (input.text == "add player kill")
//         {
//             amountIN.SetActive(true);
//             choice = 4;
//         }
//         else if (input.text == "add tower kill")
//         {
//             amountIN.SetActive(true);
//             choice = 5;
//         }
//         else if (input.text == "no amount")
//         {
//             amountIN.SetActive(false);
//         }
//         else if (input.text == "attack by assassin")
//         {
//             sfx.Soundtrack(input.text);
//         }
//         else if (input.text == "cobblestone village")
//         {
//             sfx.Soundtrack(input.text);
//         }
//         else if (input.text == "court minstrel")
//         {
//             sfx.Soundtrack(input.text);
//         }
//         else if (input.text == "wild boars inn")
//         {
//             sfx.Soundtrack(input.text);
//         }
//         else if (input.text == "skyrim")
//         {
//             sfx.Soundtrack(input.text);
//         }
//         else if (input.text == "grab1")
//         {
//             sfx.PlayerSound(input.text);
//         }
//         else if (input.text == "grab2")
//         {
//             sfx.PlayerSound(input.text);
//         }
//         else if (input.text == "coin1")
//         {
//             sfx.PlayerSound(input.text);
//         }
//         else if (input.text == "coin2")
//         {
//             sfx.PlayerSound(input.text);
//         }
//         else if (input.text == "coin3")
//         {
//             sfx.PlayerSound(input.text);
//         }
//         else if (input.text == "force1")
//         {
//             sfx.PlayerSound(input.text);
//         }
//         else if (input.text == "move wood")
//         {
//             sfx.PlayerSound(input.text);
//         }
//         else if (input.text == "release1")
//         {
//             sfx.BowSound(input.text);
//         }
//         else if (input.text == "release2")
//         {
//             sfx.BowSound(input.text);
//         }
//         else if (input.text == "release3")
//         {
//             sfx.BowSound(input.text);
//         }
//         else if (input.text == "shooting1")
//         {
//             sfx.BowSound(input.text);
//         }
//         else if (input.text == "shooting2")
//         {
//             sfx.BowSound(input.text);
//         }
//         else if (input.text == "attack1")
//         {
//             sfx.EnemiesSound(input.text);
//         }
//         else if (input.text == "attack2")
//         {
//             sfx.EnemiesSound(input.text);
//         }
//         else if (input.text == "dead1")
//         {
//             sfx.EnemiesSound(input.text);
//         }
//         else if (input.text == "dead2")
//         {
//             sfx.EnemiesSound(input.text);
//         }
//         else if (input.text == "dead3")
//         {
//             sfx.EnemiesSound(input.text);
//         }
//         else if (input.text == "ishAh")
//         {
//             sfx.EnemiesSound(input.text);
//         }
//         else if (input.text == "ooh")
//         {
//             sfx.EnemiesSound(input.text);
//         }
//         else if (input.text == "ouch")
//         {
//             sfx.EnemiesSound(input.text);
//         }
//         else if (input.text == "move1")
//         {
//             sfx.EnemiesSound(input.text);
//         }
//         else if (input.text == "move2")
//         {
//             sfx.EnemiesSound(input.text);
//         }
//         else if (input.text == "move3")
//         {
//             sfx.EnemiesSound(input.text);
//         }
//         else if (input.text == "move4")
//         {
//             sfx.EnemiesSound(input.text);
//         }
//         else if (input.text == "move5")
//         {
//             sfx.EnemiesSound(input.text);
//         }
//         else if (input.text == "spawn")
//         {
//             sfx.EnemiesSound(input.text);
//         }
//         else if (input.text == "fireplace1")
//         {
//             sfx.RoomSound(input.text);
//         }
//         else if (input.text == "fireplace2")
//         {
//             sfx.RoomSound(input.text);
//         }
//         else if (input.text == "fireplace3")
//         {
//             sfx.RoomSound(input.text);
//         }
//         else if (input.text == "fireplace4")
//         {
//             sfx.RoomSound(input.text);
//         }
//         else if (input.text == "fireplace5")
//         {
//             sfx.RoomSound(input.text);
//         }
//         else if (input.text == "garbage1")
//         {
//             sfx.RoomSound(input.text);
//         }
//         else if (input.text == "garbage2")
//         {
//             sfx.RoomSound(input.text);
//         }
//         else if (input.text == "garbage3")
//         {
//             sfx.RoomSound(input.text);
//         }
//         else if (input.text == "garbage4")
//         {
//             sfx.RoomSound(input.text);
//         }
//         else if (input.text == "tiles1")
//         {
//             sfx.RoomSound(input.text);
//         }
//         else if (input.text == "tiles2")
//         {
//             sfx.RoomSound(input.text);
//         }
//         else if (input.text == "cannon1")
//         {
//             sfx.TowerSound(input.text);
//         }
//         else if (input.text == "cannon2")
//         {
//             sfx.TowerSound(input.text);
//         }
//         else if (input.text == "explosion1")
//         {
//             sfx.TowerSound(input.text);
//         }
//         else if (input.text == "explosion2")
//         {
//             sfx.TowerSound(input.text);
//         }
//         else if (input.text == "potion1")
//         {
//             sfx.TowerSound(input.text);
//         }
//         else if (input.text == "potionBreak1")
//         {
//             sfx.TowerSound(input.text);
//         }
//         else if (input.text == "potionBreak2")
//         {
//             sfx.TowerSound(input.text);
//         }
//         else if (input.text == "potionBreak3")
//         {
//             sfx.TowerSound(input.text);
//         }
//         else if (input.text == "splash2")
//         {
//             sfx.TowerSound(input.text);
//         }
//         else if (input.text == "splash3")
//         {
//             sfx.TowerSound(input.text);
//         }
//         else if (input.text == "fireworks1")
//         {
//             sfx.WaveEndSound(input.text);
//         }
//         else if (input.text == "fireworks2")
//         {
//             sfx.WaveEndSound(input.text);
//         }
//         else if (input.text == "fireworks3")
//         {
//             sfx.WaveEndSound(input.text);
//         }
//         else if (input.text == "fireworks4")
//         {
//             sfx.WaveEndSound(input.text);
//         }
//         else if (input.text == "fireworks5")
//         {
//             sfx.WaveEndSound(input.text);
//         }
//         else if (input.text == "fireworks6")
//         {
//             sfx.WaveEndSound(input.text);
//         }
//         else if (input.text == "screaming villagers")
//         {
//             sfx.WaveEndSound(input.text);
//         }
//         else if (input.text == "trumpet")
//         {
//             sfx.WaveEndSound(input.text);
//         }
//         else if (input.text == "defenders")
//         {
//             sfx.WaveEndSound(input.text);
//         }
//         else if (input.text == "explosion3")
//         {
//             sfx.TrapSound(input.text);
//         }
//         else if (input.text == "splash1")
//         {
//             sfx.TrapSound(input.text);
//         }
//         else if (input.text == "click1")
//         {
//             sfx.TrapSound(input.text);
//         }
//         else if (input.text == "click2")
//         {
//             sfx.TrapSound(input.text);
//         }
//         else if (input.text == "click3")
//         {
//             sfx.TrapSound(input.text);
//         }
//         else if (input.text == "spike1")
//         {
//             sfx.TrapSound(input.text);
//         }
//         else if (input.text == "spike2")
//         {
//             sfx.TrapSound(input.text);
//         }
//         else if (input.text == "spike3")
//         {
//             sfx.TrapSound(input.text);
//         }
//         else if (input.text == "teleportation1")
//         {
//             sfx.TeleportaionSound(input.text);
//         }
//         else if (input.text == "teleportation2")
//         {
//             sfx.TeleportaionSound(input.text);
//         }
//     }
}
