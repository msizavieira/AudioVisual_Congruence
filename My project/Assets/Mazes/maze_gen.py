from PIL import Image

# Path to your input and output files
txt_file = "mazetxt.txt"
output_file = "maze.png"

# Read the maze text file
with open(txt_file, "r") as f:
    lines = [line.rstrip("\n") for line in f]

height = len(lines)
width = len(lines[0])

# Create a new image (white background)
img = Image.new("RGB", (width, height), "white")
pixels = img.load()

# Draw black pixels where walls are
for y, line in enumerate(lines):
    for x, char in enumerate(line):
        if char == " ":  # wall
            pixels[x, y] = (255, 255, 255)
        else:  # path
            pixels[x, y] = (0, 0, 0)

# Save image
img.save(output_file)
print(f"Saved {output_file} ({width}x{height})")
