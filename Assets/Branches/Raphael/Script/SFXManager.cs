using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField] SFXVariables sfx;

    public void StopSoundtrack1()
    {
        sfx.attackByAssassin.Stop();
    }
    public void StopSoundtrack2()
    {
        sfx.skyrim.Stop();

    }

    public void Soundtrack(string name)
    {
        if (name == "skyrim")
        {
            sfx.skyrim.Play();
        }
        else if (name == "attack by assassin")
        {
            sfx.attackByAssassin.Play();
        }
        else if (name == "cobblestone village")
        {
            sfx.cobblestoneVillage.Play();
        }
        else if (name == "court minstrel")
        {
            sfx.courtMinstrel.Play();
        }
        else if (name == "wild boars inn")
        {
            sfx.wildBoarsInn.Play();
        }

    }

    public void PlayerSound(string name)
    {
        if (name == "grab1")
        {
            sfx.grab1.Play();
        }
        else if (name == "grab2")
        {
            sfx.grab2.Play();
        }
        else if (name == "coin1")
        {
            sfx.coin1.Play();
        }
        else if (name == "coin2")
        {
            sfx.coin2.Play();
        }
        else if (name == "coin3")
        {
            sfx.coin3.Play();
        }
        else if (name == "force1")
        {
            sfx.force1.Play();
        }
        else if (name == "move wood")
        {
            sfx.moveWood.Play();
        }
    }
    public void BowSound(string name)
    {
        if (name == "release1")
        {
            sfx.release1.Play();
        }
        else if (name == "release2")
        {
            sfx.release2.Play();
        }
        else if (name == "release3")
        {
            sfx.release3.Play();
        }
        else if (name == "shooting1")
        {
            sfx.shooting1.Play();
        }
        else if (name == "shooting2")
        {
            sfx.shooting2.Play();
        }
    }
    public void EnemiesSound(string name)
    {
        if (name == "attack1")
        {
            sfx.attack1.Play();
        }
        else if (name == "attack2")
        {
            sfx.attack2.Play();
        }
        else if (name == "dead1")
        {
            sfx.dead1.Play();
        }
        else if (name == "dead2")
        {
            sfx.dead2.Play();
        }
        else if (name == "dead3")
        {
            sfx.dead3.Play();
        }
        else if (name == "ishAh")
        {
            sfx.ishAh.Play();
        }
        else if (name == "ooh")
        {
            sfx.ooh.Play();
        }
        else if (name == "ouch")
        {
            sfx.ouch.Play();
        }
        else if (name == "move1")
        {
            sfx.move1.Play();
        }
        else if (name == "move2")
        {
            sfx.move2.Play();
        }
        else if (name == "move3")
        {
            sfx.move3.Play();
        }
        else if (name == "move4")
        {
            sfx.move4.Play();
        }
        else if (name == "move5")
        {
            sfx.move5.Play();
        }
        else if (name == "spawn")
        {
            sfx.spawn.Play();
        }
    }
    public void RoomSound(string name)
    {
        if (name == "fireplace1")
        {
            sfx.fireplace1.Play();
        }
        else if (name == "fireplace2")
        {
            sfx.fireplace2.Play();
        }
        else if (name == "fireplace3")
        {
            sfx.fireplace3.Play();
        }
        else if (name == "fireplace4")
        {
            sfx.fireplace4.Play();
        }
        else if (name == "fireplace5")
        {
            sfx.fireplace5.Play();
        }
        else if (name == "garbage1")
        {
            sfx.garbage1.Play();
        }
        else if (name == "garbage2")
        {
            sfx.garbage2.Play();
        }
        else if (name == "garbage3")
        {
            sfx.garbage3.Play();
        }
        else if (name == "garbage4")
        {
            sfx.garbage4.Play();
        }
        else if (name == "tiles1")
        {
            sfx.tiles1.Play();
        }
        else if (name == "tiles2")
        {
            sfx.tiles2.Play();
        }
    }
    public void TowerSound(string name)
    {
        if (name == "cannon1")
        {
            sfx.cannon1.Play();
        }
        else if (name == "cannon2")
        {
            sfx.cannon2.Play();
        }
        else if (name == "explosion1")
        {
            sfx.explosion1.Play();
        }
        else if (name == "explosion2")
        {
            sfx.explosion2.Play();
        }
        else if (name == "potion1")
        {
            sfx.potion1.Play();
        }
        else if (name == "potionBreak1")
        {
            sfx.potionBreak1.Play();
        }
        else if (name == "potionBreak2")
        {
            sfx.potionBreak2.Play();
        }
        else if (name == "potionBreak3")
        {
            sfx.potionBreak3.Play();
        }
        else if (name == "splash2")
        {
            sfx.splash2.Play();
        }
        else if (name == "splash3")
        {
            sfx.splash3.Play();
        }
    }
    public void WaveEndSound(string name)
    {
        if (name == "fireworks1")
        {
            sfx.fireworks1.Play();
        }
        else if (name == "fireworks2")
        {
            sfx.fireworks2.Play();
        }
        else if (name == "fireworks3")
        {
            sfx.fireworks3.Play();
        }
        else if (name == "fireworks4")
        {
            sfx.fireworks4.Play();
        }
        else if (name == "fireworks5")
        {
            sfx.fireworks5.Play();
        }
        else if (name == "fireworks6")
        {
            sfx.fireworks6.Play();
        }
        else if (name == "screaming villagers")
        {
            sfx.screamingVillagers.Play();
        }
        else if (name == "trumpet")
        {
            sfx.trumpet.Play();
        }
        else if (name == "defenders")
        {
            sfx.defenders.Play();
        }
    }

    public void TrapSound(string name)
    {
        if (name == "explosion3")
        {
            sfx.explosion3.Play();
        }
        else if (name == "splash1")
        {
            sfx.splash1.Play();
        }
        else if (name == "click1")
        {
            sfx.click1.Play();
        }
        else if (name == "click2")
        {
            sfx.click2.Play();
        }
        else if (name == "click3")
        {
            sfx.click3.Play();
        }
        else if (name == "spike1")
        {
            sfx.spike1.Play();
        }
        else if (name == "spike2")
        {
            sfx.spike2.Play();
        }
        else if (name == "spike3")
        {
            sfx.spike3.Play();
        }
    }

    public void TeleportaionSound(string name)
    {
        if (name == "teleportation1")
        {
            sfx.teleportation1.Play();
        }
        else if (name == "teleportation2")
        {
            sfx.teleportation2.Play();
        }
    }


}
