using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Text_Justifer_C_sharp
{
    public partial class Form1 : Form
    {
       
        Dictionary<char, Bitmap> charBitMaps; //the bitmap for each char
       
        //the text to justify
        const string text = "Hello world i am currently typing some dummy text to test if working. Thats amazing. I know. The quick brown fox jumped over the lazy dog. Although, i am struggling to think of a long word, like expalidoucisoafdsf.";

        //Lorem ipsum test text: Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce viverra sed dolor vitae ullamcorper. Nam dictum, sapien non auctor placerat, ante eros efficitur enim, sed consequat nunc ligula eget risus. Ut aliquam tellus blandit orci mollis viverra. Ut venenatis mauris lectus. Cras finibus eu nibh non pharetra. Maecenas interdum orci sit amet odio feugiat, sed tincidunt erat euismod. Maecenas sed porta neque. Praesent dictum fringilla vulputate. Donec in convallis ipsum. Pellentesque in nisl accumsan nisl facilisis ultricies. Duis accumsan a turpis id imperdiet. Pellentesque venenatis eget lorem non egestas. Cras interdum sapien at lectus rutrum dignissim.

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int[] asciiValuesOfText = getAscOfText();

            Bitmap bmpChars = new Bitmap(@"C:\Users\skyla\OneDrive\Pictures\alphabetAndCharactersTemplate.BMP"); //the bitmap which contains the bitmap for each charactera

            int[] widthOfEachCharacter = getwidthOfEachCharacter(bmpChars);

            charBitMaps = createCharBitmaps(bmpChars);
        }

        private int[] getwidthOfEachCharacter(Bitmap bmp)
        {
            int[] widthOfEachCharacter = new int[text.Length]; //stores the width of each characrter
            for (int i = 0; i < widthOfEachCharacter.Length; i++)
            {
                widthOfEachCharacter[i] = getCharacterWidthInPixels((int)text[i], bmp);
            }

            return widthOfEachCharacter;
        }

        private int getCharacterWidthInPixels(int asciiCode, Bitmap bmpChars)
        {
            const int UpperCasePurplePixelRow = 15;
            const int LowerCasePurplePixelRow = 48;
            const int SpecialCharacterPurplePixelRow = 104;

            if (asciiCode == 32)
            {
                return -2; //indicates the character is a space, spaces size can vary, so its width would be decided later
            }
            else if (asciiCode > 64 && asciiCode < 91) //indicates character is uppercase
            {
                int howFarInAlphabet = asciiCode - 65; //0 == A, 1 == B, etc
                return widthOfCharacter(howFarInAlphabet, UpperCasePurplePixelRow, bmpChars);
            }
            else if (asciiCode > 96 && asciiCode < 123) //means character is lower case
            {
                int howFarInAlphabet = asciiCode - 97;
                return widthOfCharacter(howFarInAlphabet, LowerCasePurplePixelRow, bmpChars);
            }
            else if (asciiCode == 46) //'.' done manually for now
            {
                int howFarInspecChars = 1;
                return widthOfCharacter(howFarInspecChars, SpecialCharacterPurplePixelRow, bmpChars);
            }
            else //',' done manually for now
            {
                int howFarInSpecChars = 0;
                return widthOfCharacter(howFarInSpecChars, SpecialCharacterPurplePixelRow, bmpChars);
            }

        }

        private int widthOfCharacter(int howManyPurplePixels, int rowMovingThrough, Bitmap bitmap)
        {
            int purplePixelsPassed = 0;
            int widthOfCharacter = 0;
            bool inCharSpace = false;

            for(int i = 0; i < bitmap.Width; i++) //loop through the row containg the purple pixels.
            {
                Color colOfPixel = bitmap.GetPixel(i, rowMovingThrough);

                if (colOfPixel.R == 226 && colOfPixel.G == 57 && colOfPixel.B == 219 && colOfPixel.A == 255) //if pixel is purple
                {
                    purplePixelsPassed++;

                    if (inCharSpace)
                    {
                        return widthOfCharacter;
                    }
                    
                    if(purplePixelsPassed == howManyPurplePixels + 1)
                    {
                        inCharSpace = true;
                    }
                }

                if (inCharSpace)
                {
                    widthOfCharacter++;
                }
            }

            return -1; //this return being hit means something has gone wrong
        
        }

        //creates the dictionary for storing each bitmap
        private Dictionary<char, Bitmap> createCharBitmaps(Bitmap bmp)
        {
            const int UpperCasePurplePixelRow = 15;
            const int LowerCasePurplePixelRow = 48;
            const int SpecialCharacterPurplePixelRow = 104;

            const int UpperCaseHeight = 17;
            const int lowerCaseheight = 20;
            const int specialCaseHeight = 17;

            Dictionary<char, Bitmap> charBitMaps = new Dictionary<char, Bitmap>();
            for(int i = 0; i < 26; i++) //upper case letters
            {
                charBitMaps.Add((char)(i + 65), new Bitmap(getCharacterWidthInPixels(i + 65, bmp), UpperCaseHeight));
                formBitMap(bmp, charBitMaps[(char)(i + 65)], UpperCasePurplePixelRow, i, 2);
            }

            for(int i = 0; i < 26; i++) //lower case letters
            {
                charBitMaps.Add((char)(i + 97), new Bitmap(getCharacterWidthInPixels(i + 97, bmp), lowerCaseheight));
                formBitMap(bmp, charBitMaps[(char)(i + 97)], LowerCasePurplePixelRow, i, 0);
            }
            
            //special characters
            charBitMaps.Add((char)(44), new Bitmap(getCharacterWidthInPixels(44, bmp), specialCaseHeight));
            charBitMaps.Add((char)(46), new Bitmap(getCharacterWidthInPixels(46, bmp), specialCaseHeight));
            formBitMap(bmp, charBitMaps[(char)(44)], SpecialCharacterPurplePixelRow, 0, -1);
            formBitMap(bmp, charBitMaps[(char)(46)], SpecialCharacterPurplePixelRow, 1, -1);

            return charBitMaps;

        }

        //actually creates the bitmap for char bit maps
        private void formBitMap(Bitmap ogBitmap, Bitmap newBitMap, int RowMovingThrough, int purpleAmount, int purpleOffset)
            //Purple offset: look in bmp file; purple row of pixels is not where height should be (its too far up or too far down);
        {
            int xcord = xCoordOfOg(ogBitmap, RowMovingThrough, purpleAmount);

            for(int x = 0; x < newBitMap.Width; x++)
            {
                for(int y = 0; y < newBitMap.Height; y++)
                {
                    newBitMap.SetPixel(x, y, ogBitmap.GetPixel(x + xcord, y + ((RowMovingThrough + purpleOffset) - newBitMap.Height)));
                    //if its purple change its colour to white
                    Color colOfPixel = newBitMap.GetPixel(x, y);
                    if (colOfPixel.R == 226 && colOfPixel.G == 57 && colOfPixel.B == 219 && colOfPixel.A == 255)
                    {
                        Color white = Color.White;
                        newBitMap.SetPixel(x, y, white);
                    }
                }
            }
        }

        private int xCoordOfOg(Bitmap ogBitMap, int rowMovingThrough, int HowManyPurplePixels) //finds the x coordinate which a letter starts at
        {
            int purplesPassed = 0;
            
            for (int i = 0; i < ogBitMap.Width; i++) //loop through the row containg the purple pixels.
            {
                Color colOfPixel = ogBitMap.GetPixel(i, rowMovingThrough);

                if (colOfPixel.R == 226 && colOfPixel.G == 57 && colOfPixel.B == 219 && colOfPixel.A == 255)//if pixel is purple
                {
                    if(purplesPassed == HowManyPurplePixels)
                    {
                        return i;
                    }
                    else
                    {
                        purplesPassed++;
                    }
                }
            }

            return -1;
        }

        class Line
        {
            private int sizeOfWords = 0;
            private int amountOfWords = 0;
            List<int> eachSpaceSize = new List<int>();

            public void increamentAmountOfWords()
            {
                amountOfWords++;
            }

            public void addToSizeOfWords(int size)
            {
                sizeOfWords += size;
            }

            public int getSizeOfWords()
            {
                return sizeOfWords;
            }

            public int getAmountOfWords()
            {
                return amountOfWords;
            }

            public void CalculateEachSpaceSize(int lineLength)
            {
                int numberOfSpaces = amountOfWords - 1;
                int remainingGap = lineLength - sizeOfWords;
                int toAdd = remainingGap / numberOfSpaces;
                for (int i = 0; i < numberOfSpaces - 1; i++)
                {
                    eachSpaceSize.Add(toAdd);
                    remainingGap -= toAdd;
                }
                eachSpaceSize.Add(remainingGap);
            }

            public int getSpaceWidthAtIndex(int index)
            {
                return eachSpaceSize[index];
            }
            
        }

        private void JUSTIFY_Click(object sender, EventArgs e)
        {
            Graphics graphics = this.CreateGraphics();
            graphics.Clear(Color.White);
            int[] asciiValuesOfText = getAscOfText();
            Bitmap bmpChars = new Bitmap(@"C:\Users\skyla\OneDrive\Pictures\alphabetAndCharactersTemplate.BMP"); //the bitmap which contains the bitmap for each characters
            int[] widthOfCharacters = getwidthOfEachCharacter(bmpChars);

            const int widthOfLine = 600; //the width of a line in pixels
            const int minWidthOfSpace = 8; //the minimum width a space could be in pixels

            List<string> words = splitToWords();
            int[] eachWordSize = pixelLengthOfEachWord(words);

            List<Line> lines = new List<Line>();
            int linePointer = 0;
            lines.Add(new Line());

            for(int i = 0; i < eachWordSize.Length; i++) //caluclate how many words can fit on a line
            {
                if (lines[linePointer].getSizeOfWords() + eachWordSize[i] + minWidthOfSpace < widthOfLine)
                {
                    lines[linePointer].increamentAmountOfWords();
                    lines[linePointer].addToSizeOfWords(eachWordSize[i]);
                }
                else //means no more space in that line
                {
                    lines[linePointer].CalculateEachSpaceSize(widthOfLine);
                    linePointer++;
                    lines.Add(new Line());
                    lines[linePointer].increamentAmountOfWords();
                    lines[linePointer].addToSizeOfWords(eachWordSize[i]);
                }
            }

            lines[lines.Count - 1].CalculateEachSpaceSize(widthOfLine); //giving spaces for the last line

            //dispaly words using lines
            const int heightBetweenEachLine = 30;
            const int startX = 10;
            const int startY = 10;
            int wordsPrinted = 0;
            for(int i = 0; i < lines.Count; i++)
            {
                printLine(words, eachWordSize, wordsPrinted, startX, startY + (heightBetweenEachLine * i), lines[i]);
                wordsPrinted += lines[i].getAmountOfWords();

            }

        }
         
        private void printLine(List<string> words, int[] eachWordSize, int startOfLinePointer, int xCord, int yCord, Line line)
        {
            Graphics graphics = this.CreateGraphics();
            for(int i = 0; i <  line.getAmountOfWords() - 1; i++)
            {
                printWord(words[i + startOfLinePointer], ref xCord, yCord, graphics);
                xCord += line.getSpaceWidthAtIndex(i);
            }
            printWord(words[line.getAmountOfWords() - 1 + startOfLinePointer], ref xCord, yCord, graphics);
        }

        private void printWord(string word, ref int xCord, int yCord, Graphics graphics)
        {
            for(int i = 0; i < word.Length; i++)
            {
                graphics.DrawImage(charBitMaps[word[i]], xCord, yCord);
                xCord += charBitMaps[word[i]].Width;
            }
        }

        private int[] getAscOfText() //returns an array of all ascii values
        {
            int[] asciiValuesOfText = new int[text.Length];//holds the ascii value of each character of text in text

            for (int i = 0; i < asciiValuesOfText.Length; i++) //give asciiValuesOfText its ascii values
            {
                asciiValuesOfText[i] = (int)(text[i]);
            }

            return asciiValuesOfText;
        }

        private List<string> splitToWords() //splits the text into words
        {
            List<string> words = new List<string>();
            string consecutiveLetters = "";

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == ' ')
                {
                    words.Add(consecutiveLetters);
                    consecutiveLetters = "";
                }
                else
                {
                    consecutiveLetters += text[i];
                }
            }
            words.Add(consecutiveLetters);
            return words;
        }

        private int[] pixelLengthOfEachWord(List<string> words) //returns the pixel length of each word
        {
            int[] wordLength = new int[words.Count];
            for(int i = 0; i < words.Count; i++)
            {
                for(int j = 0; j < words[i].Length; j++)
                {
                    wordLength[i] += charBitMaps[words[i][j]].Width;
                }
            }
            return wordLength;
        }
    }
}
