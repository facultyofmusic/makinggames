using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleGame1.Source
{
    /// <summary>
    /// This is a universal resource manager that helps you manage your resources.  It only holds
    /// images and soundeffects for now.
    /// </summary>
    class ERM
    {
        private static Dictionary<String, Texture2D> images;
        private static Dictionary<String, WSoundEffect> sfxs;
        private static ContentManager contentManager;

        /// <summary>
        /// Please use this class for only static factoring. Instances of this class is not recommended
        /// </summary>
        private ERM() { }

        public static void init(ContentManager content)
        {
            
            contentManager = content;
            images = new Dictionary<string, Texture2D>();
            sfxs = new Dictionary<string, WSoundEffect>();
        }

        public static WSoundEffect getSound(String key)
        {
            return sfxs[key];
        }

        public static Texture2D getImage(String key)
        {
            return images[key];
        }


        public static void load(String[] rlist)
        {
            for (int x = 0; x < rlist.Count(); x++)
            {
                String fileName = rlist[x];
                try
                {
                    if (fileName.StartsWith("se_"))
                    {
                        fileName = @"Content\" + fileName + ".wav";
                        Debug.WriteLine("ERM>> LOADING: " + fileName);
                        sfxs.Add(rlist[x], new WSoundEffect(fileName));
                    }
                    else if (fileName.StartsWith("img_"))
                    {
                        fileName = fileName + ".png";
                        Debug.WriteLine("ERM>> LOADING: " + fileName);
                        images.Add(rlist[x], contentManager.Load<Texture2D>(fileName));
                    }
                }catch(Exception ex){
                    Debug.WriteLine(ex.StackTrace);
                    Debug.WriteLine("ERM>> FAILED: " + fileName);
                    continue;
                }
                Debug.WriteLine("ERM>> SUCCESS: " + fileName);
            }


            Debug.WriteLine("CURRENT LOADED RESOURCES:\n");

            foreach (KeyValuePair<string, Texture2D> pair in images)
            {
                Debug.WriteLine("ERM>> " + pair.Key + " ==> " + pair.Value);
            }

            foreach (KeyValuePair<string, WSoundEffect> pair in sfxs)
            {
                Debug.WriteLine("ERM>> " + pair.Key + " ==> " + pair.Value);
            }

            Debug.WriteLine("\nEND OF RESOURCE LOADING OPERATION");
        }
    }
}
