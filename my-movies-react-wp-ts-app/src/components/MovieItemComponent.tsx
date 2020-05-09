import React from "react";
import clsx from "clsx";
import {
  Button,
  Card,
  makeStyles,
  CardHeader,
  Avatar,
  IconButton,
  Collapse,
  Theme,
  createStyles,
} from "@material-ui/core";
import CardActionArea from "@material-ui/core/CardActionArea";
import CardActions from "@material-ui/core/CardActions";
import CardContent from "@material-ui/core/CardContent";
import CardMedia from "@material-ui/core/CardMedia";
import Typography from "@material-ui/core/Typography";
import { red } from "@material-ui/core/colors";
import ExpandMoreIcon from "@material-ui/icons/ExpandMore";
import MoreVertIcon from "@material-ui/icons/MoreVert";
import FavoriteIcon from "@material-ui/icons/Favorite";
import ShareIcon from "@material-ui/icons/Share";

interface MovieItemProps {
  name: string;
  description: string;
  descriptionLong: string;
  id: string;
  imageUrl: string;
  homepageUrl: string;
  dtLocalRelease: string;
  clickHandler: (event: string) => void;
}

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      maxWidth: 375,
    },
    media: {
      height: 0,
      paddingTop: "56.25%", // 16:9
    },
    expand: {
      transform: "rotate(0deg)",
      marginLeft: "auto",
      transition: theme.transitions.create("transform", {
        duration: theme.transitions.duration.shortest,
      }),
    },
    expandOpen: {
      transform: "rotate(180deg)",
    },
    avatar: {
      backgroundColor: red[500],
    },
  })
);

export default function MovieItemComponent(props: MovieItemProps) {
  const classes = useStyles();
  const [expanded, setExpanded] = React.useState(false);

  const raisePageLinkClicked = (url: string) => {
    window.open(url, "_blank");
  };

  const handleExpandClick = () => {
    setExpanded(!expanded);
  };

  return (
    <Card className={classes.root}>
      <CardHeader
        avatar={
          <Avatar aria-label="recipe" className={classes.avatar}>
            R
          </Avatar>
        }
        action={
          <IconButton
            aria-label="settings"
            onClick={() => {
              raisePageLinkClicked(props.homepageUrl);
            }}
          >
            <MoreVertIcon />
          </IconButton>
        }
        title={props.name}
        subheader={props.dtLocalRelease}
      />
      <CardMedia
        className={classes.media}
        image={props.imageUrl}
        title={props.name}
      />
      <CardContent>
        <Typography variant="body2" color="textSecondary" component="p">
          {props.description}
        </Typography>
      </CardContent>
      <CardActions disableSpacing>
        <IconButton aria-label="add to favorites">
          <FavoriteIcon />
        </IconButton>
        <IconButton aria-label="share">
          <ShareIcon />
        </IconButton>
        <IconButton
          className={clsx(classes.expand, {
            [classes.expandOpen]: expanded,
          })}
          onClick={handleExpandClick}
          aria-expanded={expanded}
          aria-label="show more"
        >
          <ExpandMoreIcon />
        </IconButton>
      </CardActions>
      <Collapse in={expanded} timeout="auto" unmountOnExit>
        <CardContent>
          <Typography paragraph>{props.descriptionLong}</Typography>
        </CardContent>
      </Collapse>
    </Card>

    // <div>
    //   <Card>
    //     <CardMedia />
    //     <CardBody>
    //       <CardTitle>{props.name}</CardTitle>
    //       <CardSubtitle>Description</CardSubtitle>
    //       <CardText>{props.description}</CardText>
    //       <Button
    //         onClick={() => {
    //           raisePageLinkClicked(props.homepageUrl);
    //         }}
    //       >
    //         Link
    //       </Button>
    //       <Button
    //         onClick={() => {
    //           props.clickHandler(props.id);
    //         }}
    //       >
    //         Remove
    //       </Button>
    //     </CardBody>
    //   </Card>
    // </div>
  );
}
