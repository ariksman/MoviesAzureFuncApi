import React from "react";
import { withStyles } from "@material-ui/core/styles";
import { green } from "@material-ui/core/colors";
import Radio, { RadioProps } from "@material-ui/core/Radio";
import { ProgressPlugin } from "webpack";

interface IMovieRadioButtonsProps {
  onToggleButtonClick: (onlyInTheaters: boolean) => void;
}

export const GreenRadio = withStyles({
  root: {
    color: green[400],
    "&$checked": {
      color: green[600],
    },
  },
  checked: {},
})((props: RadioProps) => <Radio color="default" {...props} />);

export default function InTheatersRadioButtons(props: IMovieRadioButtonsProps) {
  const [selectedValue, setSelectedValue] = React.useState("a");

  const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    if (event.target.value === "a") {
      props.onToggleButtonClick(true);
    } else {
      props.onToggleButtonClick(false);
    }
    setSelectedValue(event.target.value);
  };

  return (
    <div>
      <Radio
        checked={selectedValue === "a"}
        onChange={handleChange}
        value="a"
        name="radio-button-demo"
        inputProps={{ "aria-label": "In theaters" }}
      />
      <GreenRadio
        checked={selectedValue === "b"}
        onChange={handleChange}
        value="b"
        name="radio-button-demo"
        inputProps={{ "aria-label": "Coming soon" }}
      />
    </div>
  );
}
