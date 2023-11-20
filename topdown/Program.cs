using Microsoft.VisualBasic;
using Raylib_cs;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;

// Random generator = new Random();


// List<String> names = new List<string>() {"Martin", "Lena", "Nicholas", "Christian"};
// names.Add("Micke");
// names.Add("Yoko");




// foreach(string name in names)
// {
//     Console.WriteLine(name);
// }


// for(int i = 0; names.Count < 6; i++)
// {
//     Console.WriteLine(names[i]);
// }

// int i = 0;
// while(i < 6)
// {
//     Console.WriteLine(names[i]);
//     i++;
// }

// int i = generator.Next(names.Count);
// Console.WriteLine(names[i]);

Raylib.InitWindow(800, 600, "Hello");
Raylib.SetTargetFPS(60);


Color hotpink = new Color(255, 105, 180, 255);



// int x = 0;
Vector2 movement = new Vector2(0, 0);


Rectangle playerRect = new Rectangle(300, 400, 64, 64);
Rectangle DoorRect = new Rectangle(400, 500, 64, 64);

Texture2D playerImage = Raylib.LoadTexture("player.png");

List<Rectangle> walls = new List<Rectangle>();

walls.Add(new Rectangle(32, 32, 32, 128));
walls.Add(new Rectangle(64, 32, 128, 32));
walls.Add(new Rectangle(160, 32, 32, 128));
walls.Add(new Rectangle(256, 32, 32, 128));

float speed = 5;

string scene = "start";

int points = 0;

while(!Raylib.WindowShouldClose())
{
    if(scene == "game")
    {

        movement = Vector2.Zero;
    
        if(Raylib.IsKeyDown(KeyboardKey.KEY_D))
        {
            movement.X = 1;
        }

        if(Raylib.IsKeyDown(KeyboardKey.KEY_A))
        {
            movement.X = -1;
        }

        if(Raylib.IsKeyDown(KeyboardKey.KEY_W))
        {
            movement.Y = -1;
        }

        if(Raylib.IsKeyDown(KeyboardKey.KEY_S))
        {
            movement.Y = 1;
        }


        if (movement.Length() > 0 )
        {
            movement = Vector2.Normalize(movement) * speed;
        }


        playerRect.x += movement.X;
        playerRect.y += movement.Y;


        if(Raylib.CheckCollisionRecs(playerRect, DoorRect))
        {
            points++;
        }

        if(playerRect.x > 800 - 64 || playerRect.x < 0)
        {
            playerRect.x -= movement.X;
        }

        if(playerRect.y > 600 - 64 || playerRect.y < 0)
        {
            playerRect.y -= movement.Y;
        }

        foreach (Rectangle wall in walls)
        {
            if(Raylib.CheckCollisionRecs(playerRect, wall))
            {
                scene = "finished";
            }
        }

    }

    
    

    else if(scene == "start")
    {
        if(Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
        {
            scene = "game";
        }
    }

    Raylib.BeginDrawing();

    if(scene == "game")
    {

        Raylib.ClearBackground(Color.GOLD);
        Raylib.DrawTexture(playerImage, (int) playerRect.x, (int) playerRect.y, Color.WHITE);
        Raylib.DrawRectangleRec(DoorRect, Color.BLACK);
        Raylib.DrawText(points.ToString(), 10, 10, 32, Color.WHITE);

        foreach(Rectangle wall in walls)
        {
            Raylib.DrawRectangleRec(wall, Color.BLACK);
        }

        


    }

    else if(scene == "start")
    {
        Raylib.ClearBackground(Color.BLUE);
        Raylib.DrawText("PRESS SPACE TO START", 10, 10, 32, Color.WHITE);
    }

    else if(scene == "finished")
    {
        Raylib.ClearBackground(Color.YELLOW);
    }






    Raylib.EndDrawing();
}

