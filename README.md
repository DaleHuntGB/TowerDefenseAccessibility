# Computer Project: Colourblind Mode Accessibility

## Final Degree Project

# Goals

[x] - Protanopia Colourblind Mode.

[x] - Deuteranopia Colourblind Mode.

[x] - Tritanopia Colourblind Mode.

[x] - High Contrast Colourblind Mode.

[x] - Greyscale Colourblind Mode.

[ ] - User Control Colours via Colour Pickers.

# Approach

After research, I realised that applying filters to accommodate each colourblind setting was not efficient, therefore, I decided that I would try Colour Mapping.

The second approach (current) was to create individual Colour Profiles.

## Colour Mapping

This approach was effectively checking each material for a specific colour (default), if this would be found, it would remap to a colourblind mode friendly variant.

This worked, however, it required resetting back to defaults before the user swapped due to how the mapping was occuring. Additionally, I realised that mapping user controlled colours would be more complicated.

## Colour Profiles

This approach requires colour profiles to be assigned for each mode and then the user can merely swap between these profiles. This approach is a lot faster and it also allows for dynamic swapping. 

Additionally, this allows for me to create a custom profile that can easily be manipulated by colour pickers by merely updating the colours within the profile to match the user selected.