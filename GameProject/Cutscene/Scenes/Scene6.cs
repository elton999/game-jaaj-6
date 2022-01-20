using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Sprite;

namespace game_jaaj_6.Cutscene.Scenes
{
    public class Scene6 : GameObject
    {
        Rectangle josieBody;
        Rectangle jimBody;
        string[] words;
        Vector2[] wordsPosition;
        int currentText = 0;
        int currentChar = 0;
        int allCharNumber = 0;
        int spaceBetweenWords = 8;
        Vector2 textPosition = new Vector2(106, 185);
        Cutscene.Dialogue Dialogue;
        SpriteFont Font;

        public CutsceneManagement cutsceneManagement;

        Input InputHelper;

        public override void Start()
        {
            base.Start();
            Scene.Middleground.Add(this);
            Dialogue = new Cutscene.Dialogue();
            Sprite = Scene.Content.Load<Texture2D>("Sprites/cutscenes/cutscene1");
            Font = Scene.Content.Load<SpriteFont>("Kenney_Rocket");
            addText(currentText);

            InputHelper = new Input();
        }

        private void addText(int textIndex)
        {
            currentChar = 0;
            allCharNumber = 0;

            Line[] line = Dialogue.Castledb.Sheets[0].Lines;
            int numWords = line[textIndex].Text.Split(' ').Length;
            wordsPosition = new Vector2[numWords];

            setWords(textIndex, line);
        }

        private void setWords(int textIndex, Line[] line)
        {
            words = line[textIndex].Text.Split(' ');
            int lengthRow = 0;
            int row = 0;

            for (int i = 0; i < words.Length; i++)
            {
                int wordSize = (int)Font.MeasureString(words[i]).X;
                allCharNumber += words[i].Length;

                if (wordSize + lengthRow + spaceBetweenWords <= 213)
                {
                    wordsPosition[i] = new Vector2(lengthRow, row * spaceBetweenWords);
                    lengthRow += wordSize + spaceBetweenWords;
                }
                else
                {
                    row++;
                    wordsPosition[i] = new Vector2(0, row * spaceBetweenWords);
                    lengthRow = wordSize + spaceBetweenWords;
                }
            }
        }

        float timer = 0;
        public override void Update(GameTime gameTime)
        {
            if (cutsceneManagement.CurrentScene != 7) return;

            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer > 0.2f && currentChar < allCharNumber)
            {
                currentChar++;
                timer = 0;
            }

            input();
        }

        private void input()
        {
            if (InputHelper.KeyPress(Input.Button.CONFIRM))
            {
                if (currentChar < allCharNumber)
                    currentChar = allCharNumber;
                else {
                    currentText++;
                    if(currentText >= Dialogue.Castledb.Sheets[0].Lines.Length)
                        cutsceneManagement.CurrentScene++;
                    else addText(currentText);
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (cutsceneManagement.CurrentScene != 7) return;

            BeginDraw(spriteBatch, false);
            drawText(spriteBatch);
            EndDraw(spriteBatch);
        }

        private void drawText(SpriteBatch spriteBatch)
        {
            int totalChar = 0;
            for (int i = 0; i < words.Length; i++)
            {
                if (totalChar + words[i].Length < currentChar)
                {
                    spriteBatch.DrawString(Font, words[i], wordsPosition[i] + textPosition, SpriteColor);
                    totalChar += words[i].Length;
                }
                else
                {
                    int lengthWord = totalChar > 0 ? -(totalChar - currentChar) : currentChar;
                    spriteBatch.DrawString(Font, words[i].Substring(0, lengthWord), wordsPosition[i] + textPosition, SpriteColor);
                    i = words.Length;
                }
            }
        }
    }
}