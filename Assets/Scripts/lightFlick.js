function Update () 
    {
        if ( Random.value > 0.9 ) //a random chance
        {
           if ( GetComponent.<Light>().enabled == true ) //if the light is on...
           {
             GetComponent.<Light>().enabled = false; //turn it off
           }
           else
           {
             GetComponent.<Light>().enabled = true; //turn it on
           }
        }
    } 