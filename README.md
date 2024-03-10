# Computer Project: Colourblind Mode Accessibility

## Final Degree Project

# Goals

[x] - Protanopia Colourblind Mode.

[x] - Deuteranopia Colourblind Mode.

[x] - Tritanopia Colourblind Mode.

[x] - High Contrast Colourblind Mode.

[x] - Greyscale Colourblind Mode.

[x] - User Control Colours via Colour Pickers.

# Approach

After research, I realised that applying filters to accommodate each colourblind setting was not efficient, therefore, I decided that I would try Colour Mapping.

The second approach (current) was to create individual Colour Profiles.

## Colour Mapping

This approach was effectively checking each material for a specific colour (default), if this would be found, it would remap to a colourblind mode friendly variant.

This worked, however, it required resetting back to defaults before the user swapped due to how the mapping was occuring. Additionally, I realised that mapping user controlled colours would be more complicated.

## Colour Profiles

This approach requires colour profiles to be assigned for each mode and then the user can merely swap between these profiles. This approach is a lot faster and it also allows for dynamic swapping. 

Additionally, this allows for me to create a custom profile that can easily be manipulated by colour pickers by merely updating the colours within the profile to match the user selected.

## Screenshots

### Start Screen

![Start Screen](https://i.imgur.com/QpSdE96.png)

### Game Play

![Game Play](https://i.imgur.com/4tZddb2.png)

### Colourblind Options

![Colourblind Options](https://i.imgur.com/0ipv0IW.png)

#### Default

![Default Colours](https://i.imgur.com/Jyn3T7e.png)

#### Protanopia

![Protanopia Colours](https://i.imgur.com/Xi9LpBx.png)

#### Deuteranopia

![Deuteranopia Colours](https://i.imgur.com/zhT6PZ8.png)

#### Tritanopia

![Tritanopia Colours](https://i.imgur.com/Xi9LpBx.png)

#### High Contrast

![High Contrast Colours](https://i.imgur.com/On7S9rW.png)

#### Greyscale / Monochromatic

![Greyscale / Monochromatic Colours](https://i.imgur.com/Cqueq1P.png)

#### Custom Profile - User Options

![Custom Profile - User Options](https://i.imgur.com/gU9ghkD.png)

#### Custom Profile - User Options [Updated]

![Custom Profile - User Options](https://i.imgur.com/G5tv6JZ.png)

#### Custom Built Colour Picker

![Custom Built Colour Picker](https://i.imgur.com/tJsLXBu.gif)

#### Custom Built Colour Picker [Updated]

![Custom Built Colour Picker](https://i.imgur.com/QLkFE8R.gif)

#### Custom Profile Demonstration

![Custom Built Colour Picker](Demonstration/CustomUserProfile.gif)

### Game Tiles Colours [Health Based]

![Game Tiles Colours [Health Based]](https://i.imgur.com/hoGpe7W.gif)