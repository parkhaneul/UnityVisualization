using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueChanger : MonoBehaviour {

    public FireParticle script;

    public UISlider slider_px;
    public UISlider slider_py;
    public UISlider slider_pz;
    public UISlider slider_vx;
    public UISlider slider_vy;
    public UISlider slider_vz;
    public UISlider slider_dx;
    public UISlider slider_dy;
    public UISlider slider_dz;
    public UISlider slider_life;
    public UISlider slider_random;

    private Vector3 position;
    private Vector3 volume;
    private Vector3 direction;

    public void setPositionX()
    {
        position.x = slider_px.value * 2 - 1;
        script.position = position;
    }

    public void setPositionY()
    {
        position.y = slider_py.value * 2 - 1;
        script.position = position;
    }

    public void setPositionZ()
    {
        position.z = slider_pz.value * 2 - 1;
        script.position = position;
    }

    public void setVolumeX()
    {
        volume.x = slider_vx.value;
        script.volume = volume;
    }

    public void setVolumeY()
    {
        volume.y = slider_vy.value;
        script.volume = volume;
    }

    public void setVolumeZ()
    {
        volume.z = slider_vz.value;
        script.volume = volume;
    }

    public void setDirectionX()
    {
        direction.x = slider_dx.value * 2 - 1;
        script.direction = direction;
    }

    public void setDirectionY()
    {
        direction.y = slider_dy.value * 2 - 1;
        script.direction = direction;
    }

    public void setDirectionZ()
    {
        direction.z = slider_dz.value * 2 - 1;
        script.direction = direction;
    }

    public void setLife()
    {
        script.life = slider_life.value;
    }

    public void setRandom()
    {
        script.random = slider_random.value * 2 - 1;
    }

    public Material material;

    public UISlider slider_sr;
    public UISlider slider_sg;
    public UISlider slider_sb;
    public UISlider slider_sa;
    public UISlider slider_er;
    public UISlider slider_eg;
    public UISlider slider_eb;
    public UISlider slider_ea;

    private Color mainColor;
    private Color subColor;

    public void change_s_red()
    {
        mainColor.r = slider_sr.value;
        material.SetColor("_Main",mainColor);
    }

    public void change_s_green()
    {
        mainColor.g = slider_sg.value;
        material.SetColor("_Main", mainColor);
    }

    public void change_s_blue()
    {
        mainColor.b = slider_sb.value;
        material.SetColor("_Main", mainColor);
    }

    public void change_s_alpha()
    {
        mainColor.a = slider_sa.value;
        material.SetColor("_Main", mainColor);
    }

    public void change_e_red()
    {
        subColor.r = slider_er.value;
        material.SetColor("_Sub", subColor);
    }

    public void change_e_green()
    {
        subColor.g = slider_eg.value;
        material.SetColor("_Sub", subColor);
    }

    public void change_e_blue()
    {
        subColor.b = slider_eb.value;
        material.SetColor("_Sub", subColor);
    }

    public void change_e_alpha()
    {
        subColor.a = slider_ea.value;
        material.SetColor("_Sub", subColor);
    }
}
