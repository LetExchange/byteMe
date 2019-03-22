using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;


namespace byteMe
{
    class Program
    {
        static void Main(string[] args)
        {
            //usage byteMe <filename> <number of chunks>
            //string chunkMea = args[1];
            int numChunks = 10;    // initial entry for number of chunks
            string chunkName = "chunk";

            byte[] byteMe = File.ReadAllBytes(args[0]);  //initialize byte array with specified file contents
            FileInfo filez = new FileInfo(args[0]);   //variable for file info 

            int chunkSize = byteMe.Length / numChunks;    //byte array length divided by number of chunks
            byte[] tempByte = new byte[chunkSize];      //temporary byte array the size of the chunks to store stuff


            int chunkMod = byteMe.Length % numChunks;  //see if any bytes are leftover after integer division
            
            int finalChunk = 0;
            if (chunkMod !=0)
                finalChunk = byteMe.Length - (chunkSize * (numChunks-1));

            byte[] tempByte2 = new byte[finalChunk];   // sets up temp byte array with the size of the last chunk
        
            int bufferz = chunkSize;
            int bufferz2 = finalChunk;

            for (int i = 1; i<=numChunks; i++)
            {
                if (i < numChunks)   //if less than the number of specified chunks
                {
                   
                    //Console.WriteLine("byte array from " + (bufferz - chunkSize) + " to " + (bufferz - 1));
                    Buffer.BlockCopy(byteMe, (bufferz - chunkSize), tempByte, 0, chunkSize);
                   // Console.WriteLine("new buffer size " + tempByte.Length);
                    File.WriteAllBytes(chunkName + i, tempByte);
                    
                    bufferz += chunkSize;
                }
                else if (finalChunk != 0)  //if the final chunk is not 0, write the last chunk
                    {

                       // Console.WriteLine("final byte array from " + (bufferz - chunkSize) + " to " + (bufferz + finalChunk - chunkSize -1));
                        Buffer.BlockCopy(byteMe, (bufferz - chunkSize), tempByte2, 0, finalChunk);
                       // Console.WriteLine("new buffer size " + tempByte2.Length);
                        File.WriteAllBytes(chunkName + i, tempByte2);
                        bufferz += finalChunk;
                    }
                else   //
                    {
                      //  Console.WriteLine(" even byte array from " + (bufferz - chunkSize) + " to " + (bufferz - 1));
                        Buffer.BlockCopy(byteMe, (bufferz - chunkSize), tempByte, 0, chunkSize);
                      //  Console.WriteLine("new buffer size " + tempByte.Length);
                        File.WriteAllBytes(chunkName + i, tempByte);
                        bufferz += chunkSize;
                    }
 
            }

            //put the file back together.  Need to port over to separate script
            string newFile = "combined";
            var stream = new FileStream(newFile, FileMode.Append);
            for (int i = 1; i <= numChunks; i++)
            {
                if (i < numChunks)
                {
                    byte[] unbyteMe = File.ReadAllBytes(chunkName + i);
                    stream.Write(unbyteMe, 0, unbyteMe.Length);
                }
                else if (i == numChunks)
                {
                    byte[] unbyteMe2 = File.ReadAllBytes(chunkName + i);
                    stream.Write(unbyteMe2, 0, unbyteMe2.Length);
                }
                
            }


            }
        }

    }


// TODO
// ADD hashing, add encryption 
// add variable for num of chunks
 
