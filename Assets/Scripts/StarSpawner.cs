using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class StarSpawner : MonoBehaviour
{
    [SerializeField] private GameObject star;
    [SerializeField] private int maxStars;

    //ArrayOfColor
    Color[] starColors = {
        new Color(0.25f, 0.5f, 1f),   // Bright blue (O-type stars)
        new Color(0f, 0.75f, 1f),     // Cyan-blue
        new Color(0.3f, 1f, 0.3f),    // Soft green
        new Color(1f, 1f, 0.5f),      // Light yellow (G-type stars, like our Sun)
        new Color(1f, 0.75f, 0.2f),   // Orange-yellow (K-type stars)
        new Color(1f, 0.3f, 0.3f),    // Reddish-pink (M-type stars)
        new Color(1f, 0.1f, 0.1f),    // Deep red (Cool, dying stars)
        new Color(0.8f, 0.5f, 1f),    // Purple (Exotic, high-energy stars)
        new Color(0.4f, 0.4f, 1f),    // Indigo (Rare, hot stars)
        new Color(1f, 0.5f, 0.5f),    // Peach (Dwarf stars)
        new Color(1f, 0.7f, 0f),      // Gold (Very bright giant stars)
        new Color(0.6f, 1f, 0.6f),    // Pale green (Uncommon star types)
        new Color(0.9f, 0.9f, 1f),    // Pale blue (Subtle glow stars)
        new Color(0.7f, 0f, 1f),      // Magenta (Fictional or rare star types)
        new Color(0.3f, 0.3f, 0.3f),  // Gray (Extremely dim or dead stars)
        new Color(1f, 0.9f, 0.8f),    // Soft peach (Subtle star glow)
        new Color(0.5f, 1f, 0.7f),    // Mint green (Uncommon, faint stars)
        new Color(1f, 0.5f, 1f),      // Bright pink (Exotic stars)
        new Color(0.1f, 0.2f, 0.5f),  // Dark blue (Dim, far stars)
        new Color(1f, 0.2f, 0.8f),    // Neon pink (Bright, energetic stars)
        new Color(0.3f, 1f, 1f),      // Aquamarine (Unique, cool stars)
        new Color(1f, 1f, 1f),        // Pure white (Extremely bright stars)
        new Color(0.1f, 0.1f, 0.1f),  // Almost black (Very faint stars)
        new Color(1f, 0.6f, 0.3f),    // Copper (Uncommon, dying stars)
        new Color(0.5f, 0.5f, 1f),    // Steel blue
        new Color(0.8f, 1f, 0.5f),    // Pale lime green
        new Color(1f, 0.4f, 0.2f),    // Bright orange-red
        new Color(0.2f, 0.9f, 0.9f),  // Turquoise
        new Color(0.7f, 0.8f, 1f),    // Soft lavender-blue
        new Color(1f, 0.85f, 0.5f),   // Warm gold
        new Color(0.8f, 0.8f, 1f),    // Soft violet
        new Color(1f, 0.3f, 0.8f),    // Pink-purple mix
        new Color(0.9f, 1f, 0.6f),    // Light yellow-green
        new Color(0.5f, 0.2f, 1f),    // Deep violet
        new Color(1f, 0.7f, 0.7f),    // Rose pink
        new Color(0.2f, 0.5f, 1f),    // Cool blue
        new Color(0.9f, 0.6f, 1f),    // Light purple
        new Color(0.4f, 1f, 0.8f),    // Bright aqua
        new Color(1f, 0.2f, 0.5f),    // Deep magenta
        new Color(0.6f, 0.2f, 1f),    // Blue-violet
        new Color(0.2f, 1f, 0.9f),    // Light teal
        new Color(1f, 0.9f, 0.3f),    // Yellow-gold
        new Color(0.8f, 0.4f, 0.9f),  // Muted purple-pink
        new Color(1f, 0.8f, 0.2f),    // Bright yellow
        new Color(0.6f, 0.3f, 1f),    // Lavender-purple
        new Color(0.3f, 1f, 0.5f)     // Neon green
    };

    // Start is called before the first frame update
    void Start()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); //left-bottom
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); //top-right

        //loop for creating stars
        for (int i = 0; i < maxStars; i++)
        {
            GameObject _star = (GameObject)Instantiate(star);

            //set color
            _star.GetComponent<SpriteRenderer>().color = starColors[i % starColors.Length];

            //set random position
            _star.transform.position = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));

            //set random speed
            _star.GetComponent<StartScript>().speed = -(1f * Random.value + 0.5f);

            //make star a child of starGenerator
            _star.transform.parent = transform;

            //set random scale
            var scale = Random.Range(0.1f, 0.5f);
            _star.transform.localScale = new Vector2(scale, scale);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
