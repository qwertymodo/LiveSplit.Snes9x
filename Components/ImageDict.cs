using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace LiveSplit.Snes9x
{
    class ImageDict : Dictionary<String, List<Image>>
    {

        protected void Add(String name, String base64Image)
        {
            try
            {
                List<Image> images = new List<Image>();

                //Bitmap image = new BinaryFormatter().Deserialize(new MemoryStream(Convert.FromBase64String(base64Image))) as Bitmap;

                Bitmap image;
                var bf = new BinaryFormatter();
                var data = Convert.FromBase64String(base64Image);

                using (var ms = new MemoryStream(data))
                {
                    image = (Bitmap)bf.Deserialize(ms);
                    for (int i = 0; i < image.Width / image.Height; ++i)
                    {
                        images.Add(image.Clone(new Rectangle((i * image.Height), 0, image.Height, image.Height), image.PixelFormat));
                    }
                }

                Add(name, images);
            }
            catch (Exception) { }
        }
    }
}
